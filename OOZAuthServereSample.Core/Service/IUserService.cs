using System;
using System.Threading.Tasks;
using OOZAuthServereSample.Core.Dto;
using OOZAuthServerSample.SharedLibrary.Dto;

namespace OOZAuthServereSample.Core.Service
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);

 
    }
}
