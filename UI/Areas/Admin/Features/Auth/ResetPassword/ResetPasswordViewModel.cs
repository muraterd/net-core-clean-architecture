using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Areas.Admin.Models.Base;

namespace UI.Areas.Admin.Features.Auth.ResetPassword
{
    public class ResetPasswordViewModel : PageViewModel
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}
