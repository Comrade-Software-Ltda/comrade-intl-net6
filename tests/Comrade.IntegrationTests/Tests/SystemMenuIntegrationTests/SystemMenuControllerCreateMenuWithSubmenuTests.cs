using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerCreateMenuWithSubmenuTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemMenuControllerCreateMenuWithSubmenuTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact]
    public async Task SystemMenuController_CreateMenuWithSubmenu()
    {
        var menu = new SystemMenuCreateDto
        {
            Text = "Teste",
            Description = "Descrição do menu",
            Route = "",
            Submenus = new List<SystemMenuCreateDto>()
        };

        var subMenu = new SystemMenuCreateDto
        {
            Text = "Teste 2",
            Description = "Descrição do menu 2",
            Route = "",
        };
        menu.Submenus.Add(subMenu);
        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);

        var result = await systemMenuController.Create(menu);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(201, actualResultValue?.Code);
        }
    }
}