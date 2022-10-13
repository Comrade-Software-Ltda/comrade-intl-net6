using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemPermissionTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemPermissionIntegrationTests;

public class SystemPermissionControllerEditErrorTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemPermissionControllerEditErrorTests()
    {
        _fixture = new ServiceProviderFixture();
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemPermissionController_Edit_NullName_Error()
    {
        var id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1");
        var testObject = new SystemPermissionEditDto
        {
            Id = id,
            Name = null,
            Tag = "ACEN"
        };
        var controller = SystemPermissionInjectionController.GetSystemPermissionController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Edit(testObject);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }

        var repository = new SystemPermissionRepository(_fixture.SqlContextFixture);
        var user = await repository.GetById(id);
        Assert.NotNull(user!.Name);
    }

    [Fact]
    public async Task SystemPermissionController_Edit_NullTag_Error()
    {
        var id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1");
        var testObject = new SystemPermissionEditDto
        {
            Id = id,
            Name = "ACESSO NOVO",
            Tag = null
        };
        var controller = SystemPermissionInjectionController.GetSystemPermissionController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Edit(testObject);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }

        var repository = new SystemPermissionRepository(_fixture.SqlContextFixture);
        var user = await repository.GetById(id);
        Assert.NotNull(user!.Tag);
    }

    [Fact]
    public async Task SystemPermissionController_Edit_DuplicateName_Error()
    {
        var id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1");
        const string changeName = "ACESSO NOVO";
        const string changeTag = "  ace  ";
        var testObject = new SystemPermissionEditDto
        {
            Id = id,
            Name = changeName,
            Tag = changeTag
        };
        var controller = SystemPermissionInjectionController.GetSystemPermissionController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Edit(testObject);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }

        var repository = new SystemPermissionRepository(_fixture.SqlContextFixture);
        var user = await repository.GetById(id);
        Assert.NotEqual(changeName, user!.Name);
        Assert.NotEqual(changeTag, user!.Tag);
    }
}
