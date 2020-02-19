using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Areas.Admin.Models.Toastr;

namespace UI.Areas.Admin.Models.Base
{
    public class BaseViewModel
    {
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public ToastrModel Toastr { get; set; }

        public void ShowToastr(ToastrType type, string title, string message = null)
        {
            Toastr = new ToastrModel()
            {
                Type = type,
                Title = title,
                Message = message
            };
        }
    }
}
