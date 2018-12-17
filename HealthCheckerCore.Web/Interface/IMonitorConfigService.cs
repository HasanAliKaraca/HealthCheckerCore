using HealthCheckerCore.ApplicationCore.Entities;
using HealthCheckerCore.Web.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCheckerCore.Web.Interface
{
    public interface IMonitorConfigService
    {
        Task<int> CreateMonitorConfig(MonitorConfigViewModel vm);
        Task DeleteMonitorConfig(int id);
        Task EditMonitorConfig(MonitorConfigViewModel vm);
        Task<MonitorConfigViewModel> GetMonitorConfig(int id);
        Task<MonitorConfigIndexViewModel> GetMonitorConfigs(int pageIndex, int itemsPage);
        int GetTotalPages(int itemsPage);
    }
}
