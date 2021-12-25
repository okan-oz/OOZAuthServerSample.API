using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using OOZAuthServerSample.SharedLibrary.Dto;

namespace OOZAuthServerSample.SharedLibrary.Extensions
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();


                    ErrorDto errorDto = new ErrorDto
                    {
                        DoShow = true,
                        Errors = new List<string>()
                    {
                        errorFeature.Error.Message

                    }
                    };

                    var response = Response<NoDataDto>.Fail(errorDto,500);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));


                });
            });
        }

    }
}
