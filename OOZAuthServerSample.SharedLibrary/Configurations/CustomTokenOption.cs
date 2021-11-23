using System;
using System.Collections.Generic;

namespace OOZAuthServerSample.SharedLibrary.Configurations
{
    public class CustomTokenOption
    {
        public List<string> Audince { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshtokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
