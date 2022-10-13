using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemRoleTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemRoleIntegrationTests;

public class SystemRoleControllerEditTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemRoleControllerEditTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemRoleController_Edit()
    {
        var changeName = "NEW NAME";
        var id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        var testObject = new SystemRoleEditDto
        {
            Id = id,
            Name = changeName
        };
        var controller = SystemRoleInjectionController.GetSystemRoleController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Edit(testObject);
        if (result is ObjectResult objectResult)
        {
            var actualResultValue = objectResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(204, actualResultValue?.Code);
        }

        var repository = new SystemRoleRepository(_fixture.SqlContextFixture);
        var systemRole = await repository.GetById(id);
        Assert.Equal(changeName, systemRole!.Name);
    }
}
