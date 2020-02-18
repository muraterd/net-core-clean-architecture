using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class PhotoEntity : BaseEntity
    {
        public string FileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsProfilePhoto { get; set; }

        // Foreign Keys
        public long UserEntityId { get; set; }
    }
}
