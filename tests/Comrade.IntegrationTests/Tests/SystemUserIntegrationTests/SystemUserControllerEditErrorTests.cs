using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerEditErrorTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerEditErrorTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.PostgresContextFixture);
    }

    [Fact]
    public async Task SystemUserController_Edit_Error()
    {
        var changeName = "new Name";
        var changeEmail = "novo@email.com";
        var changeRegistration = "NovaRegistration";

        var testObject = new SystemUserEditDto
        {
            Id = 2,
            Name = changeName
        };

        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(_fixture.PostgresContextFixture, _fixture.Mediator);
        var result = await systemUserController.Edit(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }

        var repository = new SystemUserRepository(_fixture.PostgresContextFixture);
        var user = await repository.GetById(1);
        Assert.NotEqual(changeName, user!.Name);
        Assert.NotEqual(changeEmail, user.Email);
        Assert.NotEqual(changeRegistration, user.Registration);
    }
}