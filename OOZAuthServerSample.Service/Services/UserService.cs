using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OOZAuthServereSample.Core.Dto;
using OOZAuthServereSample.Core.Model;
using OOZAuthServereSample.Core.Service;
using OOZAuthServerSample.SharedLibrary.Dto;

namespace OOZAuthServerSample.Service.Services
{
    public class UserService:IUserService
    {
        private readonly UserManager<UserApp> _userManager;


        public UserService(UserManager<UserApp> userManager)
        {
           _userManager = userManager;
        }

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {

            var user = new UserApp { Email=createUserDto.Email,UserName=createUserDto.UserName};

            var result = await _userManager.CreateAsync(user,createUserDto.Password);

            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(x=>x.Description).ToList();
                return Response<UserAppDto>.Fail(new ErrorDto(errors,true),400);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user),200);
           

        }

        public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {

                return Response<UserAppDto>.Fail("Username is not found",404,true);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }
    }
}
