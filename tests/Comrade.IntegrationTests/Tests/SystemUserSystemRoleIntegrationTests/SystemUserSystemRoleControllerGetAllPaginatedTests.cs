using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using Comrade.Application.Paginations;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserSystemRoleTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserSystemRoleIntegrationTests;

public class SystemUserSystemRoleControllerGetAllPaginatedTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserSystemRoleControllerGetAllPaginatedTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemUserSystemRoleController_GetAll_Paginated()
    {
        var controller = SystemUserSystemRoleInjectionController.GetSystemUserSystemRoleController(_fixture.SqlContextFixture, _fixture.MongoDbContextFixture, _fixture.Mediator);
        var paginationQuery = new PaginationQuery();
        var result = await controller.GetAll(paginationQuery);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as PageResultDto<SystemUserSystemRoleDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
            Assert.Equal(1, actualResultValue?.Data?.Count);
        }
    }
}