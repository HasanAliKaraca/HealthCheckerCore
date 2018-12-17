using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheckerCore.ApplicationCore.Interfaces
{
    public interface IEmailSender : INotificationSender
    {
        //Task SendAsync(string email, string subject, string message);
    }
}
