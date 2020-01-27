using Application.Interfaces.Providers;
using Data;
using Data.Entities;
using Data.Models.Auth.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Providers
{
    public class TokenProvider : ITokenProvider
    {
        private readonly AppConfig appConfig;
        private readonly JwtSecurityTokenHandler tokenHandler;

        private string jwtSignKey
        {
            get
            {
                return appConfig.Auth.JwtSignKey;
            }
        }

        private DateTime expireDate
        {
            get
            {
                return DateTime.UtcNow
                    .AddDays(appConfig.Auth.JwtExpiresIn.Days)
                    .AddHours(appConfig.Auth.JwtExpiresIn.Hours)
                    .AddMinutes(appConfig.Auth.JwtExpiresIn.Minutes)
                    .AddSeconds(appConfig.Auth.JwtExpiresIn.Seconds);
            }
        }

        public TokenProvider(AppConfig appConfig)
        {
            this.appConfig = appConfig;
            tokenHandler = new JwtSecurityTokenHandler();
        }

        public SignedTokenResult Sign(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim("Email", user.Email),
            };

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Role.ToString())).ToList());

            var identity = new ClaimsIdentity(claims, "Jwt Identity");

            var key = Encoding.ASCII.GetBytes(jwtSignKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var result = new SignedTokenResult()
            {
                ExpiresIn = expireDate.ToUnixTimeStamp(),
                AccessToken = tokenHandler.WriteToken(securityToken)
            };

            return result;
        }
    }
}
