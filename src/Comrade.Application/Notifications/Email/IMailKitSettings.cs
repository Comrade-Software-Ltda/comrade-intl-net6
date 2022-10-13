namespace Comrade.Application.Notifications.Email;

public interface IMailKitSettings
{
    MailKitAuthenticateSettings? Authenticate { get; set; }
    MailKitConnectSettings? Connect { get; set; }
}
