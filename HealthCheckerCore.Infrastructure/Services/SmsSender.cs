using HealthCheckerCore.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheckerCore.Infrastructure.Services
{
    public class SmsSender : ISmsSender
    {
        public Task SendAsync(string sendTo, string subject, string message)
        {
            // TODO: Wire this up to actual sms sending logic 

            return Task.CompletedTask;
        }
    }
}
