using System;
namespace OOZAuthServereSample.Core.Model
{
    public class UserRefreshToken
    {
       public string UserId { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }

    }
}
