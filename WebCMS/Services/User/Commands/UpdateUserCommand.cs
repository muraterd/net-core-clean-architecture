using System;
namespace WebCMS.Services.User.Commands
{
    public class UpdateUserCommand
    {
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
