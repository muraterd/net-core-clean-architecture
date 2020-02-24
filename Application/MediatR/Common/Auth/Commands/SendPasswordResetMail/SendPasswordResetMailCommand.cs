using Application.Exceptions;
using Application.Interfaces.Mail;
using Application.Interfaces.Providers;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UI.Data;

namespace Application.MediatR.Common.Auth.Commands
{
    public class SendPasswordResetMailCommand : IRequest
    {
        public string Email { get; set; }
    }

    public class SendPasswordResetMailCommandHandler : IRequestHandler<SendPasswordResetMailCommand>
    {
        private readonly AppDbContext dbContext;
        private readonly IMailSender mailSender;
        private readonly AppConfig appConfig;
        private readonly IUrlProvider urlProvider;

        public SendPasswordResetMailCommandHandler(AppDbContext dbContext, IMailSender mailSender, AppConfig appConfig, IUrlProvider urlProvider)
        {
            this.dbContext = dbContext;
            this.mailSender = mailSender;
            this.appConfig = appConfig;
            this.urlProvider = urlProvider;
        }

        public async Task<Unit> Handle(SendPasswordResetMailCommand command, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(w => w.Email == command.Email && !w.IsDeleted);

            if (user == null)
            {
                throw new NotFoundException($"User not found with email {command.Email}");
            }

            user.PasswordResetToken = Guid.NewGuid().ToString().Replace("-", string.Empty);
            user.PasswordResetTokenExpireDate = DateTime.UtcNow.AddMinutes(30);

            var subject = $"{appConfig.AppName} için Şifre Sıfırlama İsteği";

            await mailSender.SendMail(
                appConfig.SMTP.From,
                user.Email,
                subject,
                GetMailBody(user.PasswordResetToken)
                );

            await dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private string GetMailBody(string passwordResetToken)
        {
            var mailBody = File.ReadAllText("Templates/ForgotPasswordMail.tr.html");
            var appUrl = urlProvider.GetBaseUrl();
            var passwordResetUrl = urlProvider.GetAbsoluteUrl("ResetPassword", "Auth", new { token = passwordResetToken });

            mailBody = mailBody.Replace("{appName}", appConfig.AppName);
            mailBody = mailBody.Replace("{appUrl}", appUrl);
            mailBody = mailBody.Replace("{passwordResetUrl}", passwordResetUrl);

            return mailBody;
        }
    }
}
