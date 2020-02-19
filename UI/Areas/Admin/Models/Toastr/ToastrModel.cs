using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Areas.Admin.Models.Toastr
{
    public class ToastrModel
    {
        public ToastrType Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public enum ToastrType
    {
        Success,
        Error
    }
}
