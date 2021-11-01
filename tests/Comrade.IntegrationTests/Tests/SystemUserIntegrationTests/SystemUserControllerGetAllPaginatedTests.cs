using Comrade.Application.Bases;
using Comrade.Application.Paginations;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using MediatR;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerGetAllPaginatedTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerGetAllPaginatedTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SystemUserController_GetAll_Paginated()
    {
        var sp = _fixture.InitiateConxtext("test_database_SystemUserController_GetAll_Paginated");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;
        InjectDataOnContextBase.InitializeDbForTests(context);

        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(context, mediator);
        var paginationQuery = new PaginationQuery();
        var result = await systemUserController.GetAll(paginationQuery);

        if (result is OkObjectResult okResult)
        {
            var actualResultValue = okResult.Value as PageResultDto<SystemUserDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
            Assert.Equal(3, actualResultValue?.Data?.Count);
        }
    }
}