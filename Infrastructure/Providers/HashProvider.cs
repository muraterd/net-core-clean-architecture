using Application.Interfaces.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Providers
{
    public class HashProvider : IHashProvider
    {
        public string Hash(string text)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(text);
        }

        public bool Verify(string text, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(text, hash);
        }
    }
}
