using System;
namespace OOZAuthServereSample.Core.Dto
{
    public class TokenDto
    {
        public string AccessToken { get; set; }

        public DateTime AccessTokenExpiration { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefrestTokenExpiration { get; set; }

    }
}
