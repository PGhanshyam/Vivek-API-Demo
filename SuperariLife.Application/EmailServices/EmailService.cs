using Microsoft.Extensions.Options;
using SuperariLife.Contracts.Settings;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SuperariLife.Application.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            try
            {
                using var message = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true
                };
                message.To.Add(toEmail);

                using var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
                {
                    EnableSsl = _emailSettings.EnableSsl,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        _emailSettings.SmtpUserName?.Trim(),
                        _emailSettings.SmtpPassword?.Replace(" ", "").Trim()
                    ),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                await smtpClient.SendMailAsync(message);
            }
            catch (SmtpException ex)
            {
                // Detailed error capture for SMTP exceptions
                throw new InvalidOperationException($"SMTP Error sending email to {toEmail}: {ex.Message}", ex);
            }
        }
    }
}
