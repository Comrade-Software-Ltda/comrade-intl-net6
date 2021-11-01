using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using MediatR;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerEditTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerEditTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SystemUserController_Edit()
    {
        var sp = _fixture.InitiateConxtext("test_database_SystemUserController_Edit");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;

        InjectDataOnContextBase.InitializeDbForTests(context);

        var changeName = "Novo Name";
        var changeEmail = "novo@email.com";
        var changePassword = "NovaPassword";
        var changeRegistration = "NovaRegistration";

        var testObject = new SystemUserEditDto
        {
            Id = 1,
            Name = changeName,
            Email = changeEmail,
            Password = changePassword,
            Registration = changeRegistration
        };


        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(context, mediator);
        var result = await systemUserController.Edit(testObject);

        if (result is ObjectResult objectResult)
        {
            var actualResultValue = objectResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(204, actualResultValue?.Code);
        }

        var repository = new SystemUserRepository(context);
        var user = await repository.GetById(1);
        Assert.Equal(changeName, user!.Name);
        Assert.Equal(changeEmail, user.Email);
        Assert.Equal(changeRegistration, user.Registration);
    }

    [Fact]
    public async Task SystemUserController_Edit_Error()
    {
        var sp = _fixture.InitiateConxtext("test_database_SystemUserController_Edit_Error");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;

        InjectDataOnContextBase.InitializeDbForTests(context);

        var changeName = "Novo Name";
        var changeEmail = "novo@email.com";
        var changeRegistration = "NovaRegistration";

        var testObject = new SystemUserEditDto
        {
            Id = 1,
            Name = changeName
        };

        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(context, mediator);
        var result = await systemUserController.Edit(testObject);

        if (result is OkObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(400, actualResultValue?.Code);
        }

        var repository = new SystemUserRepository(context);
        var user = await repository.GetById(1);
        Assert.NotEqual(changeName, user!.Name);
        Assert.NotEqual(changeEmail, user.Email);
        Assert.NotEqual(changeRegistration, user.Registration);
    }
}