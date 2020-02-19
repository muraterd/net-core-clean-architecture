using System;
namespace UI.Areas.Web.Models.Page
{
    public class PageModel : BaseModel
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
    }
}
