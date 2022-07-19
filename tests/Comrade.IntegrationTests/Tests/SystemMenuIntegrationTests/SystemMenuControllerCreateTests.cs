using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
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
    public async Task SystemMenuController_CreateFull()
    {
        var testObject = new SystemMenuCreateDto
        {
            Text = "Teste",
            Description = "Descrição do menu",
            Route = "/"
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
    public async Task SystemMenuController_CreateWithFather()
    {
        var repository = new SystemMenuRepository(_fixture.SqlContextFixture);
        var pai = new SystemMenuCreateDto
        {
            Text = "Pai",
            Description = "Descrição do menu"
        };
        var filho = new SystemMenuCreateDto
        {
            Text = "Filho",
            Description = "Descrição do menu"
        };
        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);

        var resultPai = await systemMenuController.Create(pai);

        if (resultPai is ObjectResult okResultPai)
        {
            var actualResultPai = okResultPai.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultPai);
            Assert.Equal(201, actualResultPai?.Code);
        }

        var systemMenuPai = repository.GetAll().First();
        filho.FatherId = systemMenuPai.Id;

        var resultFilho = await systemMenuController.Create(filho);

        if (resultFilho is ObjectResult okResultFilho)
        {
            var actualResultFilho = okResultFilho.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultFilho);
            Assert.Equal(201, actualResultFilho?.Code);
        }

        var systemMenuFilho = repository.GetAll().FirstOrDefault(menu => menu.FatherId == systemMenuPai.Id);
        Assert.NotNull(systemMenuFilho);
    }
}