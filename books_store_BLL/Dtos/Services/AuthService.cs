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
        private readonly EmailService _emailServise;
        private readonly JwtService _jwtService;

        public AuthService(UserManager<AppUserEntity> userManager, JwtService jwtService, EmailService emailServise)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _emailServise = emailServise;
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

            //send confirm email message
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(entity);
            byte[] bytes = Encoding.UTF8.GetBytes(token);
            string base64Token = Convert.ToBase64String(bytes);


            string root = Directory.GetCurrentDirectory();
            string templatePath = Path.Combine(root, "Storage", "Templates", "ConfirmEmail.html");
            if (!File.Exists(templatePath))
            {
                string html = await File.ReadAllTextAsync(templatePath);
                html = html.Replace("{id}", entity.Id);
                html = html.Replace("{token}", base64Token);
                _emailServise.SendEmailAsync(entity.Email, "confirm email", html, true).Wait();
            }
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
            var tokens = await _jwtService.GenerateTokensAsync(entity);

            return new ServiceResponse
            {
                Success = true,
                Message = "Successful login",
                Payload = tokens
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

        public async Task<ServiceResponse> ConfirmEmailAsync(string userId, string base64token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Invalid id: user with a such id '{userId}' isn't found"
                };
            }
            byte[] bytes = Convert.FromBase64String(base64token);
            string token = Encoding.UTF8.GetString(bytes);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = result.Errors.First().Description
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = $"the email '{user.Email}' is confirmed successfuly"
            };

        }
    }
}
