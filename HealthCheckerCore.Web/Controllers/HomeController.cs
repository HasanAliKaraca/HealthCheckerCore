using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HealthCheckerCore.Web.Models;
using Microsoft.Extensions.Logging;
using HealthCheckerCore.Web.Filter;
using HealthCheckerCore.Web.Interface;
using HealthCheckerCore.Web.ViewModel;
using System.Collections.Concurrent;
using HealthCheckerCore.ApplicationCore.Entities;
using System.Net.Http;
using HealthCheckerCore.ApplicationCore.Interfaces;

namespace HealthCheckerCore.Web.Controllers
{
    [ServiceFilter(typeof(LogFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMonitorConfigService _monitorConfigService;  
        //private readonly IHealthCheckService _healthCheckService;

        public HomeController(ILogger<HomeController> logger,
            IMonitorConfigService monitorConfigService
            //IHealthCheckService healthCheckService
            )
        {
            _logger = logger;
            _monitorConfigService = monitorConfigService;  
            //_healthCheckService = healthCheckService;
        }


        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(int? page)
        {
            var itemsPerPage = 10;

            var totalPages = _monitorConfigService.GetTotalPages(itemsPerPage);
            if (page.HasValue)
            {
                if (0 > page)
                {
                    page = 0;
                }
                else if (page > totalPages)
                {
                    page = totalPages - 1;
                }
            }
            var model = await _monitorConfigService.GetMonitorConfigs(page ?? 0, itemsPerPage);

            //_healthCheckService.StartHealthCheckJob(model.MonitorConfigs);

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MonitorConfigViewModel vm)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return View(vm);
                }

                var id = await _monitorConfigService.CreateMonitorConfig(vm);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error at creating monitor config.");

                ModelState.AddModelError("", "An internal error occured.");
                return View(vm);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _monitorConfigService.GetMonitorConfig(id);
            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MonitorConfigViewModel vm)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return View(vm);
                }

                await _monitorConfigService.EditMonitorConfig(vm);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error at updating monitor config.");

                ModelState.AddModelError("", "An internal error occured.");
                return View(vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ent = await _monitorConfigService.GetMonitorConfig(id);
                if (ent == null)
                {
                    return NotFound();
                }

                await _monitorConfigService.DeleteMonitorConfig(id);

                return Ok();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error at deleting");
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
