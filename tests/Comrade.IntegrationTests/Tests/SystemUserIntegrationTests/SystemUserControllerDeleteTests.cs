using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using System;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerDeleteTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerDeleteTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemUserController_Delete()
    {
        var systemUserId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5");

        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture, _fixture.Mediator);
        _ = await systemUserController.Delete(systemUserId);

        var repository = new SystemUserRepository(_fixture.SqlContextFixture);
        var systemUser = await repository.GetById(systemUserId);
        Assert.Null(systemUser);
    }
}