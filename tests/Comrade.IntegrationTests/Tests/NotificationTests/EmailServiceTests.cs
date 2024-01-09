using Comrade.Application.Notifications.Email;
using Xunit;

namespace Comrade.IntegrationTests.Tests.NotificationTests;

public sealed class EmailServiceTests(ServiceProviderFixture fixture) : IClassFixture<ServiceProviderFixture>
{
    //[Theory]
    //[ClassData(typeof(EmailServiceTestData))]
    public void EmailServiceTests_Test(string toEmail, string subject, string html)
    {
        var mailKitSettings = fixture.Sp.GetRequiredService<IMailKitSettings>();
        var service = new EmailService(mailKitSettings);
        service.Send(toEmail, subject, html);
        Assert.True(true);
    }
}
