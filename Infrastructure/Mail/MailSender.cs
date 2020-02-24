using Application.Interfaces.Mail;
using Data;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Mail
{
    public class MailSender : IMailSender
    {
        private readonly AppConfig appConfig;
        private readonly ILogger<MailSender> logger;

        public MailSender(AppConfig appConfig, ILogger<MailSender> logger)
        {
            this.appConfig = appConfig;
            this.logger = logger;
        }

        public async Task SendMail(string from, string to, string subject, string body, bool isBodyHtml = true, bool useSsl = true)
        {
            var SmtpSettings = appConfig.SMTP;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(from);
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = isBodyHtml;

            SmtpClient client = new SmtpClient(SmtpSettings.Host, SmtpSettings.Port)
            {
                UseDefaultCredentials = false,
                EnableSsl = useSsl,
                Credentials = new NetworkCredential(SmtpSettings.Username, SmtpSettings.Password)
            };

            logger.LogDebug($"SMTP trying to connect {SmtpSettings.Host}:{SmtpSettings.Port}");
            await client.SendMailAsync(mailMessage);

            logger.LogDebug($"Mail successfully sent to {to}");
        }
    }
}
