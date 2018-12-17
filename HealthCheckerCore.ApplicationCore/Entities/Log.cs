using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheckerCore.ApplicationCore.Entities
{
    public class Log : BaseEntity
    {
        public string Application { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string CallSite { get; set; }
        public string Exception { get; set; }
    }
}
