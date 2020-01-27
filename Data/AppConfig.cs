using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class AppConfig
    {
        public Auth Auth { get; set; } = new Auth();
    }

    public class Auth
    {
        public string JwtSignKey { get; set; }
        public JwtExpiresIn JwtExpiresIn { get; set; }
    }

    public class JwtExpiresIn
    {
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }
}
