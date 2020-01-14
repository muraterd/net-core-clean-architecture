using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public List<UserRoleEntity> Roles { get; set; }
    }
}