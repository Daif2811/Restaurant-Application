using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Restaurant.Models;
using System.Threading.Tasks;

namespace Restaurant.IRepository.Repository
{
    public class EmailSender : IEmailSender
    {
        


        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            mimeMessage.To.Add(MailboxAddress.Parse(email)); // Use Parse method

            mimeMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                client.Authenticate(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                client.Send(mimeMessage);
                client.Disconnect(true);
            }

            return Task.CompletedTask;
        }

    }
}
