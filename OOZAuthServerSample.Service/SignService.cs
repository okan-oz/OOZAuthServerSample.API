using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OOZAuthServerSample.Service
{
    public static class SignService
    {

        public static SecurityKey GetSymetricSecurityKey(string key)
        {

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key) );
        }
    }
}
