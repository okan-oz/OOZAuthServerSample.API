using System;
using System.Threading.Tasks;
using OOZAuthServereSample.Core.Dto;
using OOZAuthServerSample.SharedLibrary.Dto;

namespace OOZAuthServereSample.Core.Service
{
    public interface IAuthenticatonService
    {
        Task<Response<TokenDto>> CreateToken(LoginDto loginDto);

        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

        Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken);

        Task<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);

    }
}
