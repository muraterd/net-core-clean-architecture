using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.Auth.Jwt
{
    public class SignedTokenResult
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
