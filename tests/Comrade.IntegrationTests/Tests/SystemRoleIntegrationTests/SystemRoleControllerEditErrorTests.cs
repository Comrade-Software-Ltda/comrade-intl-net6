using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemRoleTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemRoleIntegrationTests;

public class SystemRoleControllerEditErrorTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemRoleControllerEditErrorTests()
    {
        _fixture = new ServiceProviderFixture();
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemRoleController_Edit_NullName_Error()
    {
        var id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        var testObject = new SystemRoleEditDto
        {
            Id = id,
            Name = null
        };
        var controller = SystemRoleInjectionController.GetSystemRoleController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Edit(testObject);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }

        var repository = new SystemRoleRepository(_fixture.SqlContextFixture);
        var user = await repository.GetById(id);
        Assert.NotNull(user!.Name);
    }

    [Fact]
    public async Task SystemRoleController_Edit_DuplicateName_Error()
    {
        var changeName = " Creator ";
        var id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        var testObject = new SystemRoleEditDto
        {
            Id = id,
            Name = changeName
        };
        var controller = SystemRoleInjectionController.GetSystemRoleController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Edit(testObject);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }

        var repository = new SystemRoleRepository(_fixture.SqlContextFixture);
        var user = await repository.GetById(id);
        Assert.NotEqual(changeName, user!.Name);
    }
}
