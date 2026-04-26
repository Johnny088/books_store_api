using books_store_BLL.Dtos.Auth;
using books_store_BLL.Dtos.Genre;
using books_store_BLL.Dtos.Services;
using books_store_BLL.Settings;
using books_store_DAL.Entities;
using books_store_DAL.Entities.identity;
using books_store_DAL.Migrations;
using books_store_DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace books_store_BLL.Dtos.Services
{
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<AppUserEntity> _userManager;
        private readonly RefreshTokenRepository _RefreshTokenRepository;
        public JwtService(IOptions<JwtSettings> jwtOptions, UserManager<AppUserEntity> userManager, RefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtOptions.Value;
            _userManager = userManager;
            _RefreshTokenRepository = refreshTokenRepository;
        }
        private RefreshTokenEntity GenerateRefreshToken()
        {
            var bytes = new byte[64];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            string token = Convert.ToBase64String(bytes);
            return new RefreshTokenEntity
            {
                Token = token,
                ExpiresData = DateTime.UtcNow.AddDays(7),
            };
        }

        public async Task<JwtDto> GenerateTokensAsync(AppUserEntity user)
        {
            var accessToken = await GenerateAccessTokenAsync(user);
            var refreshToken = GenerateRefreshToken();
            refreshToken.UserId = user.Id;
            await _RefreshTokenRepository.CreateAsync(refreshToken);
            return new JwtDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<ServiceResponse> RefreshAsync(string refreshToken)
        {
            var oldToken = await _RefreshTokenRepository.GetByTokenAsync(refreshToken);
            if (oldToken == null || oldToken.IsExpired || oldToken.IsUsed)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "something went wrong. Refresh token doesn't work."
                };
            }
            var user = await _userManager.FindByIdAsync(oldToken.UserId);
            if(user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "something went wrong"
                };
            }

            oldToken.IsUsed = true;

            await _RefreshTokenRepository.UpdateAsync(oldToken);

            var tokens = await GenerateTokensAsync(user);
            return new ServiceResponse
            {
                Success = true,
                Message = "Tokens are created successfuly",
                Payload = tokens
            };
        }

        public async Task<string> GenerateAccessTokenAsync(AppUserEntity user)
        {
            if (string.IsNullOrEmpty(_jwtSettings.SecretKey))
            {
                throw new ArgumentException("Jwt secret key is null");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim("userName",user.UserName?? string.Empty),
                new Claim("email", user.Email??  string.Empty),
                new Claim("name", user.Name ??  string.Empty),
                new Claim("lastName", user.LastName ?? string.Empty),
                new Claim("image", user.Image ?? string.Empty),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }
            var secretKeyBytes = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var signInKey = new SymmetricSecurityKey(secretKeyBytes);
            var credentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddHours(_jwtSettings.ExpHours)
                );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
        public async void CleanTokensAsync()
        {
            var tokens = await _RefreshTokenRepository.GetAll().ToListAsync();

            if (tokens != null) return;

            foreach(var token in tokens)
            {
                if(DateTime.Now - token.CreateDate > TimeSpan.FromDays(7))
                {
                    await _RefreshTokenRepository.DeleteAsync(token);
                }
            }
        }
    }
}