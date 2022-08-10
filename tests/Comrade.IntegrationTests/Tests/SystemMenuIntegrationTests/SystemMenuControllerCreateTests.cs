using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Domain.Models;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerCreateTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemMenuControllerCreateTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SystemMenuController_Create()
    {
        var testObject = new SystemMenuCreateDto
        {
            Text = "Teste",
            Description = "Descrição do menu"
        };

        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);

        var result = await systemMenuController.Create(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(201, actualResultValue?.Code);
        }
    }

    [Fact]
    public async Task SystemMenuController_CreateMenuWithSubMenu()
    {
        var menu = new SystemMenuCreateDto
        {
            Text = "Teste",
            Description = "Descrição do menu",
            Route = "/",
            Submenus = new List<SystemMenuCreateDto>()
        };

        var subMenu = new SystemMenuCreateDto
        {
            Text = "Teste 2",
            Description = "Descrição do menu 2",
            Route = "/menu2"
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

    [Fact]
    public async Task SystemMenuController_CreateSubMenu()
    {
        var repository = new SystemMenuRepository(_fixture.SqlContextFixture);
        var menu = new SystemMenu
        {
            Id = Guid.NewGuid(),
            Text = "Menu",
            Description = "Descrição do menu"
        };
        var subMenu = new SystemMenuCreateDto
        {
            Text = "Submenu",
            Description = "Descrição do submenu"
        };
        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);

        _fixture.SqlContextFixture.SystemMenus.Add(menu);
        subMenu.MenuId = menu.Id;
        var resultFilho = await systemMenuController.Create(subMenu);

        if (resultFilho is ObjectResult okResultFilho)
        {
            var actualResultFilho = okResultFilho.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultFilho);
            Assert.Equal(201, actualResultFilho?.Code);
        }
        var subMenus = repository.GetAll()
            .Where(x => x.MenuId == menu.Id);
        Assert.NotNull(subMenus);
        Assert.Equal(1, subMenus.Count());
    }
}