using Comrade.Application.Bases;
using Comrade.Application.Paginations;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerGetAllPaginatedTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerGetAllPaginatedTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemUserController_GetAll_Paginated()
    {
        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);
        var paginationQuery = new PaginationQuery();
        var result = await systemUserController.GetAll(paginationQuery);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as PageResultDto<SystemUserDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
            Assert.Equal(4, actualResultValue?.Data?.Count);
        }
    }
}