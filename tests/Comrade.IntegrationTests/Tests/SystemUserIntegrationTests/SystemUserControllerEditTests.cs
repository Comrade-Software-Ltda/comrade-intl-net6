using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerEditTests
{
    [Fact]
    public async Task SystemUserController_Edit()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_SystemUserController_Edit")
            .EnableSensitiveDataLogging().Options;

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

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);
        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(context);
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
    public async Task Edit_SystemUser_Error()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_Edit_SystemUser_Error")
            .EnableSensitiveDataLogging().Options;

        var changeName = "Novo Name";
        var changeEmail = "novo@email.com";
        var changeRegistration = "NovaRegistration";

        var testObject = new SystemUserEditDto
        {
            Id = 1,
            Name = changeName
        };

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(context);
        var result = await systemUserController.Edit(testObject);

        if (result is OkObjectResult okObjectResult)
        {
            var actualResultValue = okObjectResult.Value as SingleResultDto<EntityDto>;
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