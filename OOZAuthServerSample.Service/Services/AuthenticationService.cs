using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OOZAuthServereSample.Core.Configuration;
using OOZAuthServereSample.Core.Dto;
using OOZAuthServereSample.Core.Model;
using OOZAuthServereSample.Core.Repositories;
using OOZAuthServereSample.Core.Service;
using OOZAuthServereSample.Core.Service.UnitOfWork;
using OOZAuthServerSample.SharedLibrary.Dto;

namespace OOZAuthServerSample.Service.Services
{
    public class AuthenticationService : IAuthenticatonService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AuthenticationService(IOptions<List<Client>> optionsClients ,ITokenService tokenService,UserManager<UserApp> userManager, IUnitOfWork unitOfWork ,IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _clients = optionsClients.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }


        public async  Task<Response<TokenDto>> CreateToken(LoginDto loginDto)
        {
           if(loginDto == null)
            {
                throw new ArgumentException(nameof(loginDto));
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            { 
                return Response<TokenDto>.Fail("Email or password is wrong", 401, true); ;
            }

            if(await _userManager.CheckPasswordAsync(user, loginDto.Password) == false)
            {
                return Response<TokenDto>.Fail("Email or password is wrong", 401, true); ;
            }

            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {

                await _userRefreshTokenService.AddAsync(new UserRefreshToken{ UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefrestTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefrestTokenExpiration;
            }

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(token,200);

        }

        public   Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x=>x.Id==clientLoginDto.ClientId && x.Secret==clientLoginDto.ClientSecret);

            if (client == null)
            {
                return Response<ClientTokenDto>.Fail("ClientId or Client secret not found",404,true);
            }

            var token = _tokenService.CreateTokenByClient(client);

            return Response<ClientTokenDto>.Success(token,200);

        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefrehToken = await _userRefreshTokenService.Where(x=>x.Code==refreshToken).SingleOrDefaultAsync();

            if (existRefrehToken == null)
            {
                return Response<TokenDto>.Fail("Refresh token is not found",404,true);
            }


            var user = await _userManager.FindByIdAsync(existRefrehToken.UserId);

            if (user == null)
            {
                return Response<TokenDto>.Fail("Refresh token is not found", 404, true);
            }

            var newToken = _tokenService.CreateToken(user);

            existRefrehToken.Code = newToken.RefreshToken;
            existRefrehToken.Expiration = newToken.RefrestTokenExpiration;

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(newToken ,200);
        }

        public async Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return Response<NoDataDto>.Fail("Refresh token is not found",404,true);
            }

            _userRefreshTokenService.Remove(existRefreshToken) ;

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(200);
        }
    }
}
