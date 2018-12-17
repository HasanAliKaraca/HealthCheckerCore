using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheckerCore.Web.ViewModel
{
    public class MonitorConfigIndexViewModel
    {
        public IEnumerable<MonitorConfigViewModel> MonitorConfigs { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
     
}
