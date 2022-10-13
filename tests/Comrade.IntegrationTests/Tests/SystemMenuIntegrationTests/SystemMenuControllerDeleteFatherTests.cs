using System;
using Comrade.Application.Bases;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerDeleteMenuTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemMenuControllerDeleteMenuTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemMenuController_ExistentMenu_Delete()
    {
        var systemMenuId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a8");

        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);


        var result = await systemMenuController.Delete(systemMenuId);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.Equal(2, _fixture.SqlContextFixture.SystemMenus.Count());
        }
    }
}
