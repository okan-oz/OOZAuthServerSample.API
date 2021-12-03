using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using OOZAuthServerSample.SharedLibrary.Configurations;
using OOZAuthServerSample.SharedLibrary.Services;
namespace OOZAuthServerSample.SharedLibrary.Extensions
{
    public static class CustomTokenAuth
    {
        public static void AddCustomTokenAuth(this IServiceCollection services,CustomTokenOption tokenOptions)
        {
            services.AddAuthentication(
               options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

               }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
               {
                  
                   opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       ValidIssuer = tokenOptions.Issuer,
                       ValidAudience = tokenOptions.Audince[0],
                       IssuerSigningKey = SignService.GetSymetricSecurityKey(tokenOptions.SecurityKey),
                       ValidateIssuerSigningKey = true,
                       ValidateAudience = true,
                       ValidateIssuer = true,
                       ValidateLifetime = true,
                       ClockSkew = System.TimeSpan.Zero

                   };


               });
        }
    }
}
