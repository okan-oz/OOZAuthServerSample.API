﻿using System;
namespace OOZAuthServereSample.Core.Dto
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
