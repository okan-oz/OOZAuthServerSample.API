using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using OOZAuthServereSample.Core.Dto;
using OOZAuthServereSample.Core.Service;

namespace OOZAuthServerSample.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticatonService _authenticatonService;

        public AuthController(IAuthenticatonService authenticatonService)
        {
            _authenticatonService = authenticatonService;
        }

        //api/aut/
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticatonService.CreateToken(loginDto);

            return ActionResultInstance(result);

        }


        [HttpPost]
        public  IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result =   _authenticatonService.CreateTokenByClient(clientLoginDto);
            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshtoken(RefreshTokenDto refreshToken)
        {
            var result =await _authenticatonService.RevokeRefreshToken(refreshToken.Token);
            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result =await _authenticatonService.CreateTokenByRefreshToken(refreshTokenDto.Token);
            return ActionResultInstance(result);
        }
    }
}