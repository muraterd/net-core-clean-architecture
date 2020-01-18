using Data.Entities;
using Data.Models.Auth.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Providers
{
    public interface ITokenProvider
    {
        SignedTokenResult Sign(UserEntity user);

        JwtModel Decode(string token);
    }
}
