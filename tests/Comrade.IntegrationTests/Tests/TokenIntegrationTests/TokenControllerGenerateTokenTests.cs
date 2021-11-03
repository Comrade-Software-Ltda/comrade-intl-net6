using Comrade.Application.Bases;
using Comrade.Application.Services.AuthenticationServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using System;
using Xunit;

namespace Comrade.IntegrationTests.Tests.TokenIntegrationTests;

public sealed class TokenControllerGenerateTokenTests
{
    [Fact]
    public async Task TokenController_GenerateToken()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_TokenController_GenerateToken")
            .EnableSensitiveDataLogging().Options;


        var testObject = new AuthenticationDto
        {
            Key = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
            Password = "123456"
        };

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var tokenController = TokenInjectionController.GetTokenController(context);
        var result = await tokenController.GenerateToken(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<UserDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
        }
    }

    [Fact]
    public async Task TokenController_GenerateToken_Error()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_TokenController_GenerateToken_Error")
            .EnableSensitiveDataLogging().Options;


        var testObject = new AuthenticationDto
        {
            Key = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
            Password = "Error"
        };

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var tokenController = TokenInjectionController.GetTokenController(context);
        var result = await tokenController.GenerateToken(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<UserDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(1001, actualResultValue?.Code);
        }
    }
}