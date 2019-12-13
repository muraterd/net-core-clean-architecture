using System;
namespace WebCMS.Areas.Web.Models
{
    public class BaseModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
