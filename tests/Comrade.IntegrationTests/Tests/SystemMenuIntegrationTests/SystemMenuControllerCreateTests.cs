using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerCreateTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemMenuControllerCreateTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SystemMenuController_Create()
    {
        var testObject = new SystemMenuCreateDto
        {
            Title = "Teste",
            Description = "Descrição do menu",
            Route = ""
        };

        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);

        var result = await systemMenuController.Create(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(201, actualResultValue?.Code);
        }
    }
}
