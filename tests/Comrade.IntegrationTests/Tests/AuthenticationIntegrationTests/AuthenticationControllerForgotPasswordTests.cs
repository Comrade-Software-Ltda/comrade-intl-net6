#region

using Comrade.Application.Bases;
using Comrade.Application.Services.AuthenticationServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.AuthenticationIntegrationTests;

public sealed class AuthenticationControllerForgotPasswordTests
{

    [Fact]
    public async Task AuthenticationController_ForgotPassword()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_AuthenticationController_ForgotPassword")
            .EnableSensitiveDataLogging().Options;


        var testObject = new AuthenticationDto
        {
            Key = "1",
            Password = "123456"
        };

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var authenticationController =
            AuthenticationInjectionController.GetAuthenticationController(context);
        var result = await authenticationController.ForgotPassword(testObject);

        if (result is OkObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
        }
    }
}
