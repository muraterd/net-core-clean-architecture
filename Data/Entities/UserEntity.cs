using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<UserRoleEntity> Roles { get; set; } = new List<UserRoleEntity>();

        // Getters
        public string FullName
        {
            get { return $"{FirstName ?? String.Empty} {LastName ?? String.Empty}".Trim(); }
        }
    }
}