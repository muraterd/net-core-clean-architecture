using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Areas.Admin.Models.Base
{
    public class ListPageViewModel<T> : BaseViewModel
    {
        public IList<T> List { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPageCount { get; set; }
    }
}
