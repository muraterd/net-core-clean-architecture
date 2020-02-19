using Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Areas.Admin.Models.Base;

namespace UI.Areas.Admin.Features.Users.Profile
{
    public class ProfileViewModel : PageViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public PhotoEntity ProfilePhoto { get; set; }
        public IFormFile ProfilePicture { get; set; }

        // Getters
        public string FullName
        {
            get { return $"{FirstName ?? String.Empty} {LastName ?? String.Empty}".Trim(); }
        }
    }
}
