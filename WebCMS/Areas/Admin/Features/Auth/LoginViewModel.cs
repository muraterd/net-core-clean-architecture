using System;
using System.ComponentModel.DataAnnotations;

namespace WebCMS.Areas.Admin.Features.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi girin")]
        [Display(Name = "E-Posta", Prompt = "E-Posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "Şifre", Prompt = "Şifre")]
        public string Password { get; set; }
    }
}
