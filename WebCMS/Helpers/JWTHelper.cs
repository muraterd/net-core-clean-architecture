using System;
using JWT.Algorithms;
using JWT.Builder;
using WebCMS.Data.Entities;

namespace WebCMS.Helpers
{
    public class JwtHelper
    {
        private static readonly string Secret = "Çok acayip secret";

        public static SignedTokenResult Sign(UserEntity user)
        {
            var result = new SignedTokenResult();

            var expireDate = DateTime.Now.AddMonths(1);

            result.ExpiresIn = expireDate.ToUnixTimeStamp();
            result.Token = new JwtBuilder()
                                .WithAlgorithm(new HMACSHA256Algorithm())
                                .ExpirationTime(expireDate)
                                .WithVerifySignature(true)
                                .WithSecret(Secret)
                                .AddClaim("id", user.Id)
                                .Build();
            return result;
        }

        public static JwtModel Decode(string token)
        {
            return new JwtBuilder()
                    .WithSecret(Secret)
                    .MustVerifySignature()
                    .Decode<JwtModel>(token);
        }
    }

    public class SignedTokenResult
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }

    public class JwtModel
    {
        public int Exp { get; set; }
        public int Id { get; set; }
    }
}
