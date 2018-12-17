using HealthCheckerCore.ApplicationCore.Enums;
using HealthCheckerCore.ApplicationCore.Interfaces;
using HealthCheckerCore.Web.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCheckerCore.Web.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public NotificationService(IEmailSender emailSender,
           ISmsSender smsSender)
        {
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

        public async Task SendNotification(List<NotificationType> notificationTypesToCall, string sendTo, string subject, string message)
        {
            if (notificationTypesToCall == null)
            {
                return;
            }

            foreach (var enm in notificationTypesToCall)
            {
                switch (enm)
                {
                    case NotificationType.Email:
                        await _emailSender.SendAsync(sendTo, subject, message);
                        break;
                    case NotificationType.Sms:
                        await _smsSender.SendAsync(sendTo, subject, message);
                        break;
                    default:
                        break;
                }
            }

        }

    }
}

