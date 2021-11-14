using System;
using OOZAuthServereSample.Core.Configuration;
using OOZAuthServereSample.Core.Dto;
using OOZAuthServereSample.Core.Model;

namespace OOZAuthServereSample.Core.Service
{
    public interface ITokenService
    {

        TokenDto CreateToken(UserApp userApp);

        ClientTokenDto CreateTokenByClient(Client client);
    }
}
