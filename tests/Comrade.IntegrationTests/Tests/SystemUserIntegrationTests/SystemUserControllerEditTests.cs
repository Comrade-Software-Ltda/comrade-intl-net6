using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using System;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerEditTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerEditTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemUserController_Edit()
    {
        var changeName = "new name";
        var changeEmail = "novo@email.com";
        var changePassword = "NovaPassword";
        var changeRegistration = "NovaRegistration";

        var systemUserId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5");

        var testObject = new SystemUserEditDto
        {
            Id = systemUserId,
            Name = changeName,
            Email = changeEmail,
            Password = changePassword,
            Registration = changeRegistration
        };


        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);
        var result = await systemUserController.Edit(testObject);

        if (result is ObjectResult objectResult)
        {
            var actualResultValue = objectResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(204, actualResultValue?.Code);
        }

        var repository = new SystemUserRepository(_fixture.SqlContextFixture);
        var user = await repository.GetById(systemUserId);
        Assert.Equal(changeName, user!.Name);
        Assert.Equal(changeEmail, user.Email);
        Assert.Equal(changeRegistration, user.Registration);
    }
}