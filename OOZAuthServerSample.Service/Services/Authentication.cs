using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
    public class Authentication : IAuthenticatonService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshToken;

        public Authentication(IOptions<List<Client>> optionsClients ,ITokenService tokenService,UserManager<UserApp> userManager, IUnitOfWork unitOfWork ,IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _clients = optionsClients.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshToken = userRefreshTokenService;
        }


        public Task<Response<TokenDto>> CreateToken(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
