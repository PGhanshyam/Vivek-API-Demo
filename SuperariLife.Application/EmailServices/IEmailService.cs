using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperariLife.Application.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlBody);
    }
}
