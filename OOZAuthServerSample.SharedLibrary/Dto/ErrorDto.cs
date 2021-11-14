using System;
using System.Collections.Generic;

namespace OOZAuthServerSample.SharedLibrary.Dto
{
    public class ErrorDto
    {
        public  List<string> Errors { get; set; }
        public bool DoShow { get; set; }

        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public ErrorDto(string error ,bool doShow)
        {
            Errors.Add(error);
            DoShow = doShow;
        }

        public ErrorDto(List<string> errors,bool doShow)
        {
            Errors = errors;
            DoShow = doShow;
        }
    }
}
