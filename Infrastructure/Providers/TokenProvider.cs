using Application.Interfaces.Providers;
using Data;
using Data.Entities;
using Data.Models.Auth.Jwt;
using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Providers
{
    public class TokenProvider : ITokenProvider
    {
        private readonly AppConfig appConfig;
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
        }

        public JwtModel Decode(string token)
        {
            return new JwtBuilder()
                    .WithSecret(jwtSignKey)
                    .MustVerifySignature()
                    .Decode<JwtModel>(token);
        }

        public SignedTokenResult Sign(UserEntity user)
        {
            var result = new SignedTokenResult();

            result.ExpiresIn = expireDate.ToUnixTimeStamp();
            result.AccessToken = new JwtBuilder()
                                .WithAlgorithm(new HMACSHA256Algorithm())
                                .ExpirationTime(expireDate)
                                .WithVerifySignature(true)
                                .WithSecret(jwtSignKey)
                                .AddClaim("id", user.Id)
                                .Build();
            return result;
        }
    }
}
