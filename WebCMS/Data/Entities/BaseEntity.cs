using System;
namespace WebCMS.Data.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
        }
    }
}
