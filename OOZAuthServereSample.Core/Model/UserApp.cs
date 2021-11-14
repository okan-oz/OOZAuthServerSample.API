using System;
using Microsoft.AspNetCore.Identity;

namespace OOZAuthServereSample.Core.Model
{
    public class UserApp:IdentityUser
    {
        public string City { get; set; }
       
    }
}
