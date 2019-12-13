using System;
using System.ComponentModel.DataAnnotations;

namespace WebCMS.Areas.Api.Features.Auth.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email eksik")]
        [EmailAddress(ErrorMessage = "Bu ne aq email yollaman lazım")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre eksik")]
        public string Password { get; set; }
    }
}
