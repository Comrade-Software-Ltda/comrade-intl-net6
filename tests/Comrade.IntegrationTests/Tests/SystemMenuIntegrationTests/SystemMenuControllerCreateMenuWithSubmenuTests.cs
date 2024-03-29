using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenu.Contracts;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerCreateMenuWithSubmenuTests(ServiceProviderFixture fixture)
    : IClassFixture<ServiceProviderFixture>
{
    [Fact]
    public async Task SystemMenuController_CreateMenuWithSubmenu()
    {
        var menu = new SystemMenuCreateDto
        {
            Title = "Teste",
            Icon = "icon",
            Description = "Descri��o do menu",
            Route = "",
            Submenus = new List<SystemMenuCreateDto>()
        };

        var subMenu = new SystemMenuCreateDto
        {
            Title = "Teste",
            Icon = "icon",
            Description = "Descri��o do menu 2",
            Route = ""
        };
        menu.Submenus.Add(subMenu);
        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(fixture.SqlContextFixture,
                fixture.MongoDbContextFixture,
                fixture.Mediator);

        var result = await systemMenuController.Create(menu);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(201, actualResultValue?.Code);
        }
    }
}
