using HealthCheckerCore.ApplicationCore.Entities;

namespace HealthCheckerCore.ApplicationCore.Specifications
{
    public class MonitorConfigFilterPaginatedSpecification : BaseSpecification<MonitorConfig>
    {
        public MonitorConfigFilterPaginatedSpecification(int skip, int take)
            : base(w => true)
        {
            ApplyPaging(skip, take);
        }
    }
}
