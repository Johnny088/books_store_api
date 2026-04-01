using books_store_BLL.Dtos.Auth;
using books_store_DAL.Entities.identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.Dtos.Services
{
    public class AuthService
    {
        private readonly UserManager<AppUserEntity> _userManager;

        public AuthService(UserManager<AppUserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ServiceResponse> RegisterAsync(RegisterDto dto)
        {
            if (await EmailExistAsync(dto.Email))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"invalid email: email: '{dto.Email}'  is already used",
                };
            }
            if (await UserNameExistAsync(dto.UserName))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"invalid email: email: '{dto.UserName}'  is already used",
                };
            }
            var entity = new AppUserEntity
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Name = dto.Name,
                LastName = dto.LastName,
            };
            var response = await _userManager.CreateAsync(entity, dto.Password);
            if (!response.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = response.Errors.First().Description
                };
            }
            _userManager.AddToRoleAsync(entity, "user").Wait();
            return new ServiceResponse
            {
                Success = true,
                Message = "User is created successfuly",
                Payload = entity.Name
            };

        }
        public async Task<ServiceResponse> LoginAsync(LoginDto dto)
        {
            var entity = await _userManager.FindByEmailAsync(dto.Email);
            if(entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Invalid email: '{dto.Email}' isn't exist"
                };
            }
            var result = await _userManager.CheckPasswordAsync(entity, dto.Password);
            if (!result)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"The password is incorrect"
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = "Successful login",
                Payload = null
            };
        }



        private async Task<bool> EmailExistAsync(string email)
        {
           return await _userManager.FindByEmailAsync(email) != null;
        }
        private async Task<bool> UserNameExistAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName) != null;
        }
        
    }
}
