using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Areas.Admin.Models.Base;

namespace UI.Areas.Admin.Features.Auth.ForgotPassword
{
    public class ForgotPasswordViewModel : PageViewModel
    {
        public string Email { get; set; }
    }
}
