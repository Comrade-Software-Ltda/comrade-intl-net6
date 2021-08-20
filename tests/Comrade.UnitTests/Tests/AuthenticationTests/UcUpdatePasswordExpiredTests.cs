#region

using Comrade.Domain.Extensions;
using Comrade.Domain.Models;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests;

public sealed class UcUpdatePasswordTests
{
    private readonly ITestOutputHelper _output;
    private readonly UcAuthenticationInjection _ucAuthenticationInjection = new();

    public UcUpdatePasswordTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task UcUpdatePassword_Test()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_UcUpdatePassword_Test")
            .EnableSensitiveDataLogging().Options;


        var testObject = new SystemUser(1, "111", "777@testObject",
            "100.SdwfwU4tDWbBkLlBNd7Vcg==.cGEYFjBRNpLrCxzYNIbSdnbbY1zFvBHcyIslMTSmwy8=", "123",
            DateTimeBrasilia.GetDateTimeBrasilia());


        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var repository = new SystemUserRepository(context);
        var returnBefore = await repository.GetById(testObject.Id);
        var passwordBefore = returnBefore!.Password;

        var ucUpdatePassword =
            _ucAuthenticationInjection.GetUcUpdatePassword(context);
        var result = await ucUpdatePassword.Execute(testObject);
        _output.WriteLine(result.Message);

        var returnAfter = await repository.GetById(testObject.Id);
        var passwordAfter = returnAfter!.Password;

        Assert.NotEqual(passwordBefore, passwordAfter);
    }
}
