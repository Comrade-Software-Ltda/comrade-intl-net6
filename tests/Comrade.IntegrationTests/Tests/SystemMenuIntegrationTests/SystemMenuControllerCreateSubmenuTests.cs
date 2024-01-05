using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenu.Contracts;
using Comrade.Domain.Models;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerCreateSubmenuTests(ServiceProviderFixture fixture)
    : IClassFixture<ServiceProviderFixture>
{
    [Fact]
    public async Task SystemMenuController_CreateSubmenu()
    {
        var repository = new SystemMenuRepository(fixture.SqlContextFixture);
        var menu = new SystemMenu
        {
            Id = Guid.NewGuid(),
            Title = "Menu",
            Icon = "home",
            Description = "Descrição do menu",
            Route = ""
        };
        var subMenu = new SystemMenuCreateDto
        {
            Title = "Submenu",
            Icon = "home",
            Description = "Descrição do submenu",
            Route = ""
        };
        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(fixture.SqlContextFixture,
                fixture.MongoDbContextFixture,
                fixture.Mediator);

        fixture.SqlContextFixture.SystemMenus.Add(menu);
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
