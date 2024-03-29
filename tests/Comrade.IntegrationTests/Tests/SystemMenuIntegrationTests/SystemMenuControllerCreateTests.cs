using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenu.Contracts;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerCreateTests(ServiceProviderFixture fixture)
    : IClassFixture<ServiceProviderFixture>
{
    [Fact]
    public async Task SystemMenuController_Create()
    {
        var testObject = new SystemMenuCreateDto
        {
            Title = "Teste",
            Description = "Descri��o do menu",
            Route = ""
        };

        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(fixture.SqlContextFixture,
                fixture.MongoDbContextFixture,
                fixture.Mediator);

        var result = await systemMenuController.Create(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(201, actualResultValue?.Code);
        }
    }
}
