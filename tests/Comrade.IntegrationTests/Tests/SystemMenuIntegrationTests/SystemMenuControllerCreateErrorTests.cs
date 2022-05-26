using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerCreateErrorTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemMenuControllerCreateErrorTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SystemMenuController_Create_Error()
    {
        var testObject1 = new SystemMenuCreateDto
        {
            Text = "Teste",
            Route = "/"
        };
        var testObject2 = new SystemMenuCreateDto
        {
            Description = "Descrição do teste",
            Route = "/"
        };

        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);

        var result1 = await systemMenuController.Create(testObject1);
        var result2 = await systemMenuController.Create(testObject2);

        if (result1 is ObjectResult okResult1)
        {
            var actualResultValue = okResult1.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }

        if (result2 is ObjectResult okResult2)
        {
            var actualResultValue = okResult2.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }
    }
}