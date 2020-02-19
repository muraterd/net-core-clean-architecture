using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCMS.Models
{
    public class ProjectSetupResult
    {
        public DBSetupResult DBSetupResult = new DBSetupResult();
    }

    public enum SetupStatus
    {
        Failed,
        Success
    }

    public class DBSetupResult
    {
        public SetupStatus Status { get; set; }

        public SetupStatus AdminAccount { get; set; }

        public SetupStatus Settings { get; set; }
    }
}
