using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Enums
{
    public enum RoleType
    {
        [Description("Acayip Süper Admin")]
        SuperAdmin,
        Admin,
        Editor,
        [Description("Normal User")]
        User
    }
}
