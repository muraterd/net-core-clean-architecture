using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class UserRoleEntity
    {
        public long Id { get; set; }
        public RoleType Role { get; set; }
    }
}
