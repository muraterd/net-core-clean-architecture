using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Areas.Admin.Features.Users.Requests
{
    public class UpdateUserRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
