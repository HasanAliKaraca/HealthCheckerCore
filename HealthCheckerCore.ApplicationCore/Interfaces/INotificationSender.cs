using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheckerCore.ApplicationCore.Interfaces
{
    public interface INotificationSender
    {
        Task SendAsync(string sendTo, string subject, string message);
    }
}
