using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Application.Paginations;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerGetAllMenusTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemMenuControllerGetAllMenusTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }


    [Fact]
    public async Task SystemMenuController_GetAll()
    {
        var paginationQuery = new PaginationQuery();
        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);

        var result = await systemMenuController.GetAllMenus(paginationQuery);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as PageResultDto<SystemMenuDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
            Assert.Equal(3, actualResultValue?.Data?.Count);

            var oneMenu = actualResultValue?.Data?
                .FirstOrDefault(dto => dto.Id.Equals(Guid.Parse("6adf10d0-1b83-46f2-91eb-0c64f1c638a8")));

            Assert.Equal(2, oneMenu?.Submenus?.Count);
        }
    }
}