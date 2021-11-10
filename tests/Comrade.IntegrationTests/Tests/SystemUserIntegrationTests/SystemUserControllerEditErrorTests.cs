using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using System;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerEditErrorTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerEditErrorTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemUserController_Edit_Error()
    {
        var changeName = "new Name";
        var changeEmail = "novo@email.com";
        var changeRegistration = "NovaRegistration";

        var systemUserId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5");

        var testObject = new SystemUserEditDto
        {
            Id = systemUserId,
            Name = changeName
        };

        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);
        var result = await systemUserController.Edit(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }

        var repository = new SystemUserRepository(_fixture.SqlContextFixture);
        var user = await repository.GetById(systemUserId);
        Assert.NotEqual(changeName, user!.Name);
        Assert.NotEqual(changeEmail, user.Email);
        Assert.NotEqual(changeRegistration, user.Registration);
    }
}