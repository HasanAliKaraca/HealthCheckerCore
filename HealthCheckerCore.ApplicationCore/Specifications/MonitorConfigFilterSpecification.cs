using HealthCheckerCore.ApplicationCore.Entities;

namespace HealthCheckerCore.ApplicationCore.Specifications
{
    public class MonitorConfigFilterSpecification : BaseSpecification<MonitorConfig>
    {
        public MonitorConfigFilterSpecification()
            : base(w => true)
        {
        }
    }
}
