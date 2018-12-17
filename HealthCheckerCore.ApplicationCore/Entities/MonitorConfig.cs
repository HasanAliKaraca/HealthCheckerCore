using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheckerCore.ApplicationCore.Entities
{
    public class MonitorConfig : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public TimeSpan Interval { get; set; }
        public bool IsDown { get; set; }
        public DateTime LastCheckDate { get; set; }
    }
}
