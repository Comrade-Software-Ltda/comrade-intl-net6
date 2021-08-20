#region

using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public sealed class SystemUserControllerCreateTests
{
    private readonly SystemUserInjectionController _systemUserInjectionController = new();

    [Fact]
    public async Task SystemUserController_Create()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_SystemUserController_Create")
            .EnableSensitiveDataLogging().Options;


        var testObject = new SystemUserCreateDto
        {
            Name = "111",
            Email = "777@testObject",
            Password = "123456",
            Registration = "123"
        };


        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        var systemUserController =
            _systemUserInjectionController.GetSystemUserController(context);
        _ = await systemUserController.Create(testObject);
        Assert.Equal(1, context.SystemUsers.Count());
    }


    [Fact]
    public async Task SystemUserController_Create_Error()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_SystemUserController_Create_Error")
            .EnableSensitiveDataLogging().Options;

        var testObject = new SystemUserCreateDto
        {
            Email = "777@testObject",
            Password = "123456",
            Registration = "123"
        };

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        var systemUserController =
            _systemUserInjectionController.GetSystemUserController(context);
        var result = await systemUserController.Create(testObject);

        if (result is OkObjectResult okObjectResult)
        {
            var actualResultValue = okObjectResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(400, actualResultValue?.Code);
        }

        Assert.False(context.SystemUsers.Any());
    }
}
