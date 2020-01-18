using Application.Interfaces.Providers;
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
        private static readonly string Secret = "Çok acayip secret";

        public JwtModel Decode(string token)
        {
            return new JwtBuilder()
                    .WithSecret(Secret)
                    .MustVerifySignature()
                    .Decode<JwtModel>(token);
        }

        public SignedTokenResult Sign(UserEntity user)
        {
            var result = new SignedTokenResult();

            var expireDate = DateTime.Now.AddMonths(1);

            result.ExpiresIn = expireDate.ToUnixTimeStamp();
            result.AccessToken = new JwtBuilder()
                                .WithAlgorithm(new HMACSHA256Algorithm())
                                .ExpirationTime(expireDate)
                                .WithVerifySignature(true)
                                .WithSecret(Secret)
                                .AddClaim("id", user.Id)
                                .Build();
            return result;
        }
    }
}
