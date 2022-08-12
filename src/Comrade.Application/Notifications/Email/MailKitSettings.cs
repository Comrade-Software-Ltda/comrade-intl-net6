namespace Comrade.Application.Notifications.Email;

public class MailKitSettings: IMailKitSettings
{
    public MailKitAuthenticateSettings? Authenticate { get; set; }
    public MailKitConnectSettings? Connect { get; set; }
}