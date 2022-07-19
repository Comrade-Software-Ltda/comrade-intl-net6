using System.Resources;
using System.Text;
using Comrade.Application.Messages;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Comrade.Application.Notifications.Email
{
    public class EmailService: IEmailService
    {
        private readonly IMailKitSettings _mailKitSettings;
        public EmailService(IMailKitSettings mailKitSettings)
        {
            _mailKitSettings = mailKitSettings;
        }

        public void Send(string toEmail, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(ApplicationMessage.NOTIFICATION_DEFAULT_NAME,
                _mailKitSettings.Authenticate?.UserName));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_mailKitSettings.Connect?.Host, _mailKitSettings.Connect.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailKitSettings.Authenticate?.UserName, _mailKitSettings.Authenticate?.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
            email.Dispose();
        }
    }
}
