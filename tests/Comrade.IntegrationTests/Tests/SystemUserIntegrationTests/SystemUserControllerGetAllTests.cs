#region

using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerGetAllTests
{
    private readonly SystemUserInjectionController _systemUserInjectionController = new();

    [Fact]
    public async Task SystemUserController_GetAll()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_SystemUserController_GetAll")
            .EnableSensitiveDataLogging().Options;

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var systemUserController =
            _systemUserInjectionController.GetSystemUserController(context);
        var result = await systemUserController.GetAll(null);

        if (result is OkObjectResult okObjectResult)
        {
            var actualResultValue = okObjectResult.Value as PageResultDto<SystemUserDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
            Assert.Equal(4, actualResultValue?.Data?.Count);
        }
    }
}
