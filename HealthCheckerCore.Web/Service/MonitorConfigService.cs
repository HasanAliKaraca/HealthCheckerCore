using HealthCheckerCore.ApplicationCore.Entities;
using HealthCheckerCore.ApplicationCore.Interfaces;
using HealthCheckerCore.ApplicationCore.Specifications;
using HealthCheckerCore.Web.Interface;
using HealthCheckerCore.Web.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheckerCore.Web.Service
{
    public class MonitorConfigService : IMonitorConfigService
    {
        private readonly ILogger<MonitorConfigService> _logger;
        private readonly IRepository<MonitorConfig> _repository;
        private readonly IAsyncRepository<MonitorConfig> _asyncRepository;

        public MonitorConfigService(IRepository<MonitorConfig> repository, ILogger<MonitorConfigService> logger, IAsyncRepository<MonitorConfig> asyncRepository)
        {
            _logger = logger;
            _repository = repository;
            _asyncRepository = asyncRepository;
        }

        public async Task<int> CreateMonitorConfig(MonitorConfigViewModel vm)
        {
            _logger.LogInformation("CreateMonitorConfig called.");

            var entity = new MonitorConfig()
            {
                Name = vm.Name,
                Interval = new TimeSpan(0, 0, vm.Interval),
                Url = vm.Url
            };

            var result = await _asyncRepository.AddAsync(entity);
            _logger.LogInformation($"Entity created, id: {entity.Id}");

            return result.Id;
        }

        public async Task<MonitorConfigViewModel> GetMonitorConfig(int id)
        {
            _logger.LogInformation("CreateMonitorConfig called.");

            var vm = new MonitorConfigViewModel();

            var entity = await _asyncRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }

            vm.Name = entity.Name;
            vm.Url = entity.Url;
            vm.Interval = (int)Math.Round(entity.Interval.TotalSeconds);
            vm.Id = entity.Id;

            return vm;
        }

        public async Task EditMonitorConfig(MonitorConfigViewModel vm)
        {
            _logger.LogInformation("EditMonitorConfig called.");

            var entity = await _asyncRepository.GetByIdAsync(vm.Id);
            if (entity == null)
            {
                return;
            }

            entity.Name = vm.Name;
            entity.Interval = new TimeSpan(0, 0, vm.Interval);
            entity.Url = vm.Url;

            await _asyncRepository.UpdateAsync(entity);
            _logger.LogInformation($"Entity updated, id: {entity.Id}");
        }

        public async Task DeleteMonitorConfig(int id)
        {
            _logger.LogInformation("DeleteMonitorConfig called.");

            var entity = await _asyncRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return;
            }

            await _asyncRepository.DeleteAsync(entity);
            _logger.LogInformation($"Entity deleted, id: {entity.Id}");
        }

        public int GetTotalPages(int itemsPage)
        {
            var filterSpecification = new MonitorConfigFilterSpecification();

            var totalItems = _repository.Count(filterSpecification);

            var totalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString());

            return totalPages;
        }

        public async Task<MonitorConfigIndexViewModel> GetMonitorConfigs(int pageIndex, int itemsPage)
        {
            _logger.LogInformation("GetMonitorConfigs called.");

            var filterSpecification = new MonitorConfigFilterSpecification();
            var filterPaginatedSpecification = new MonitorConfigFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage);

            var itemsOnPage = _repository.List(filterPaginatedSpecification).ToList();
            var totalItems = _repository.Count(filterSpecification);

            var vm = new MonitorConfigIndexViewModel()
            {
                MonitorConfigs = itemsOnPage.Select(w => new MonitorConfigViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Url = w.Url,
                    Interval = (int)Math.Round(w.Interval.TotalSeconds)
                }),
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.Count,
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                }
            };

            return vm;
        }

    }
}
