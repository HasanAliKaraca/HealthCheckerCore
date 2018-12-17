using HealthCheckerCore.ApplicationCore.Enums;
using HealthCheckerCore.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheckerCore.Web.Interface
{
    public interface INotificationService
    {
        Task SendNotification(List<NotificationType> notificationTypesToCall, string sendTo, string subject, string message);
    }
}
