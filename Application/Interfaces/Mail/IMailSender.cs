using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Mail
{
    public interface IMailSender
    {
        Task SendMail(string from, string to, string subject, string body, bool isBodyHtml = true, bool useSsl = true);
    }
}
