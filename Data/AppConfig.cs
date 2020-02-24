using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class AppConfig
    {
        public string AppName { get; set; }
        public Auth Auth { get; set; } = new Auth();
        public SMTP SMTP { get; set; }
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

    public class SMTP
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public bool UseSsl { get; set; }
    }
}
