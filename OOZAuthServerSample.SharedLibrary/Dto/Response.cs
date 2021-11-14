using System;
using System.Text.Json.Serialization;

namespace OOZAuthServerSample.SharedLibrary.Dto
{
    public class Response<T> where T:class
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public ErrorDto Error { get; set; }

        [JsonIgnore]
        public   bool IsSuccessful { get; set; }

        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true};
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { StatusCode = statusCode ,Data=default,IsSuccessful=true};
        }

        public static Response<T> Fail(ErrorDto error , int statusCode)
        {
            return new Response<T>
            {
                Error = error,
                StatusCode = statusCode,
                IsSuccessful=false

            };
        }

        public static Response<T> Fail(string errorMessage, int statusCode,bool doShow)
        {
            ErrorDto error = new ErrorDto(errorMessage,doShow);
            return new Response<T>
            {
                Error = error,
                StatusCode = statusCode,
                IsSuccessful=false
            };
        }


    }
}
