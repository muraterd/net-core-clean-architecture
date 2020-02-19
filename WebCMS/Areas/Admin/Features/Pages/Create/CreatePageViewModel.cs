using WebCMS.Areas.Admin.Models.Base;

namespace WebCMS.Areas.Admin.Features.Pages.Create
{
    public class CreatePageViewModel : PageViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool? IsActive { get; set; }
    }
}
