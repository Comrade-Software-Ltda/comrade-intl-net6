using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Core.Messages;
using Comrade.Domain.Models;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerCreateDuplicatedMenuErrorTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemMenuControllerCreateDuplicatedMenuErrorTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SystemMenuController_CreateDuplicatedMenu_Error()
    {
        var menu1 = new SystemMenu
        {
            Title = "Menu",
            Icon = "home",
            Description = "Descrição do menu",
            Route = ""
        };
        var menu2 = new SystemMenuCreateDto
        {
            Title = "Menu",
            Icon = "home",
            Description = "Descrição do menu",
            Route = ""
        };
        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);

        _fixture.SqlContextFixture.SystemMenus.Add(menu1);
        await _fixture.SqlContextFixture.SaveChangesAsync();

        var resultFilho = await systemMenuController.Create(menu2);

        if (resultFilho is ObjectResult okResultFilho)
        {
            var actualResultFilho = okResultFilho.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultFilho);
            Assert.Equal(409, actualResultFilho?.Code);
            Assert.Equal(BusinessMessage.MSG20, actualResultFilho?.Message);
        }
    }
}
