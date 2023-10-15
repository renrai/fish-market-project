using FishMarketProjectDomain.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectService.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private static string emailSender = "fishmarkettoken@outlook.com";
        private static string emailSenderPass = "fishmarket!";

        public Task SendEmailAsync(string email, string token)
        {
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(emailSender, emailSenderPass)
            };
            return client.SendMailAsync(new MailMessage(from: emailSender, to: email, subject: "Fish Market Validation Token", body: $"Here is your token to validate your register: {token}"));
        }
    }
}
