using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.User.Commands
{
    public class CreateUserCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
