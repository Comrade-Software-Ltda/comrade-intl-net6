using Comrade.Application.Messages;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Comrade.Application.Notifications.Email;

public class EmailService(IMailKitSettings mailKitSettings) : IEmailService
{
    public void Send(string toEmail, string subject, string html)
    {
        // create message
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(ApplicationMessage.NOTIFICATION_DEFAULT_NAME,
            mailKitSettings.Authenticate?.UserName));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) {Text = html};

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect(mailKitSettings.Connect?.Host, mailKitSettings.Connect.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(mailKitSettings.Authenticate?.UserName, mailKitSettings.Authenticate?.Password);
        smtp.Send(email);
        smtp.Disconnect(true);
        email.Dispose();
    }
}
