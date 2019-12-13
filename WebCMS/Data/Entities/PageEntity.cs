using System;
namespace WebCMS.Data.Entities
{
    public class PageEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
    }
}
