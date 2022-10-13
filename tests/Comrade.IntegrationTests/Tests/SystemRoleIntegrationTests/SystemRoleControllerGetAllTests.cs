using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemRoleTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemRoleIntegrationTests;

public class SystemRoleControllerGetAllTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemRoleControllerGetAllTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemRoleController_GetAll()
    {
        var controller = SystemRoleInjectionController.GetSystemRoleController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.GetAll(null);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as PageResultDto<SystemRoleDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
            Assert.Equal(2, actualResultValue?.Data?.Count);
        }
    }
}
