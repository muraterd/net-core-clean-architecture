using System;
using System.ComponentModel.DataAnnotations;

namespace WebCMS.Areas.Admin.Models.Page
{
    public class BasePageModel : BaseModel
    {
        public string Title { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir")]
        public string Slug { get; set; }
    }
}
