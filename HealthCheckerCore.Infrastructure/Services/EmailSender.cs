using HealthCheckerCore.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace HealthCheckerCore.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendAsync(string sendTo, string subject, string message)
        {
            // TODO: Wire this up to actual email sending logic via SendGrid, local SMTP, etc.

            return Task.CompletedTask;
        }
    }
}
