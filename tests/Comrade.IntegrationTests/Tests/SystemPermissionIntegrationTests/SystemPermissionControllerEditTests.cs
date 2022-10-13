using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemPermissionTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemPermissionIntegrationTests;

public class SystemPermissionControllerEditTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemPermissionControllerEditTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemPermissionController_Edit()
    {
        var id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1");
        const string name = "ACESSO NOVO";
        const string tag = "ACEN";
        var testObject = new SystemPermissionEditDto
        {
            Id = id,
            Name = name,
            Tag = tag
        };
        var controller = SystemPermissionInjectionController.GetSystemPermissionController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Edit(testObject);
        if (result is ObjectResult objectResult)
        {
            var actualResultValue = objectResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(204, actualResultValue?.Code);
        }

        var repository = new SystemPermissionRepository(_fixture.SqlContextFixture);
        var obj = await repository.GetById(id);
        Assert.Equal(name, obj!.Name);
        Assert.Equal(tag, obj!.Tag);
    }
}
