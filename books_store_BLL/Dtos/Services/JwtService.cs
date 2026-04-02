using books_store_BLL.Settings;
using books_store_DAL.Entities.identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace books_store_BLL.Dtos.Services
{
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtOptions)
        {
           _jwtSettings = jwtOptions.Value;
        }

        public string GenerateAccessToken(AppUserEntity user)
        {
            if (string.IsNullOrEmpty(_jwtSettings.SecretKey))
            {
                throw new ArgumentException("Jwt secret key is null");
            }
            var claims = new Claim[]
            {
                new Claim("userName",user.UserName?? string.Empty),
                new Claim("email", user.Email??  string.Empty),
                new Claim("name", user.Name ??  string.Empty),
                new Claim("lastName", user.LastName ?? string.Empty),
                new Claim("image", user.Image ?? string.Empty)

            };
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
    }
}
