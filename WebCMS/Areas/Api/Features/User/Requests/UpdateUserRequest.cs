using System;
using System.ComponentModel.DataAnnotations;

namespace WebCMS.Areas.Api.Features.User.Requests
{
    public class UpdateUserRequest
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
