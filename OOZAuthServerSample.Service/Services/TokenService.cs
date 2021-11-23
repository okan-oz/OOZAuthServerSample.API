using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OOZAuthServereSample.Core.Configuration;
using OOZAuthServereSample.Core.Dto;
using OOZAuthServereSample.Core.Model;
using OOZAuthServereSample.Core.Service;
using OOZAuthServerSample.SharedLibrary.Configurations;

namespace OOZAuthServerSample.Service.Services
{
    public class TokenService:ITokenService
     {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _tokenOption;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }


        /// <summary>
        /// 32 bytelık guid bir rakam oluşturur
        /// </summary>
        /// <returns></returns>
        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            throw new NotImplementedException();
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
