using HealthCheckerCore.ApplicationCore.Entities;
using HealthCheckerCore.ApplicationCore.Enums;
using HealthCheckerCore.Infrastructure.Data;
using HealthCheckerCore.Web.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheckerCore.Web.Service
{
    internal class TimedHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private static ConcurrentQueue<MonitorConfig> cq;
        private static HttpClient client = new HttpClient();

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly INotificationService _notificationService;

        public TimedHostedService(ILogger<TimedHostedService> logger,
            INotificationService notificationService,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _notificationService = notificationService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            await DoWork(null);
        }

        private async Task MakeRequest(MonitorConfig item)
        {
            while (true)
            {
                string url = item.Url;
                var id = item.Id;

                try
                {
                    _logger.LogInformation($"Requesting Url: {url}.");

                    //query url list if its down
                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode == false)
                    {
                        _logger.LogInformation($"Url is down: {url}.");

                        //send notification
                        //this can be changed to read from some configuration
                        var notificationList = new List<NotificationType>();
                        notificationList.Add(NotificationType.Email);
                        notificationList.Add(NotificationType.Sms);

                        await _notificationService.SendNotification(notificationList, "customerInfo", "Site is down", "Site is down");
                    }
                }
                catch (HttpRequestException e)
                {
                    //if site is not exist we get this error
                    _logger.LogError(e, $"HttpRequestException. URL: {url}");
                    break;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "");
                    throw;
                }

                await Task.Delay(item.Interval);
            }

        }
        
        private async Task DoWork(object state)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<HealthCheckerContext>())
                {
                    var list = dbContext.Set<MonitorConfig>().ToList();
                    cq = new ConcurrentQueue<MonitorConfig>(list);
                }
            }

            foreach (var item in cq)
            {
                await Task.Run(async () => MakeRequest(item));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            return Task.CompletedTask;
        }
         
    }
}
