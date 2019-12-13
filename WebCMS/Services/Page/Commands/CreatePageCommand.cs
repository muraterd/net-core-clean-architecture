using System;
namespace WebCMS.Services.Page.Commands
{
    public class CreatePageCommand
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
