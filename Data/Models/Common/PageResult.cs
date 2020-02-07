using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.Common
{
    public class PageResult<T>
    {
        public IList<T> List { get; set; }
        public int TotalPageCount { get; set; }
    }
}
