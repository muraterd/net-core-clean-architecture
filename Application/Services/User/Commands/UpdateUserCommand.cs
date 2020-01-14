using System;
namespace Application.Services.User.Commands
{
    public class UpdateUserCommand
    {
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
