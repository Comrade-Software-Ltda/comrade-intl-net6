using Comrade.Application.Notifications.Email;
using Comrade.IntegrationTests.Tests.NotificationTests.TestDatas;
using Xunit;

namespace Comrade.IntegrationTests.Tests.NotificationTests;

public sealed class EmailServiceTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;
    public EmailServiceTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [ClassData(typeof(EmailServiceTestData))]
    public void EmailServiceTests_Test(string toEmail, string subject, string html)
    {
        var mailKitSettings = _fixture.Sp.GetRequiredService<IMailKitSettings>();
        var service = new EmailService(mailKitSettings);
        service.Send(toEmail, subject, html);
        Assert.True(true);
    }
}