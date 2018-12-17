using HealthCheckerCore.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheckerCore.Infrastructure.Data
{
    public class MonitorConfigRepository : EfRepository<MonitorConfig>, IMonitorConfigRepository
    {
        public MonitorConfigRepository(HealthCheckerContext dbContext) : base(dbContext)
        {

        }
    }
}
