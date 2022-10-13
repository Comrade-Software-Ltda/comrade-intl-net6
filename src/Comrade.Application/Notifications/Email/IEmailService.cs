namespace Comrade.Application.Notifications.Email;

public interface IEmailService
{
    void Send(string toEmail, string subject, string html);
}
