using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class CurrentUser
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PhotoEntity ProfilePhoto { get; set; }
        public List<string> Roles { get; set; }

        // Getters
        public string FullName
        {
            get { return $"{FirstName ?? String.Empty} {LastName ?? String.Empty}".Trim(); }
        }
    }
}
