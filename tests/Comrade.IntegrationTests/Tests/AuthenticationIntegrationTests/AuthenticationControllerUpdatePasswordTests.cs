using Comrade.Application.Bases;
using Comrade.Application.Services.AuthenticationServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AuthenticationIntegrationTests;

public sealed class AuthenticationControllerUpdatePasswordTests
{
    [Fact]
    public async Task AuthenticationController_UpdatePassword()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_AuthenticationController_UpdatePassword")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .EnableSensitiveDataLogging().Options;


        var testObject = new AuthenticationDto
        {
            Key = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
            Password = "123456"
        };

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var authenticationController =
            AuthenticationInjectionController.GetAuthenticationController(context);
        var result = await authenticationController.UpdatePassword(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
        }
    }
}