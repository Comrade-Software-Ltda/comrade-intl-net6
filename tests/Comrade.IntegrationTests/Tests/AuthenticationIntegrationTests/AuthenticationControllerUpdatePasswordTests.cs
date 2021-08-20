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

public sealed class AuthenticationControllerUpdatePasswordTests
{
    private readonly AuthenticationInjectionController _authenticationInjectionController =
        new();

    [Fact]
    public async Task AuthenticationController_UpdatePassword()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_AuthenticationController_UpdatePassword")
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
            _authenticationInjectionController.GetAuthenticationController(context);
        var result = await authenticationController.UpdatePassword(testObject);

        if (result is OkObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
        }
    }
}
