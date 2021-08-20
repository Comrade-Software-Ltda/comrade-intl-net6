#region

using Comrade.Application.Services.AuthenticationServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Comrade.UnitTests.Tests.AuthenticationTests.TestDatas;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests;

public sealed class UcGenerateTokenTests

{
    private readonly ITestOutputHelper _output;
    private readonly UcAuthenticationInjection _ucAuthenticationInjection = new();

    public UcGenerateTokenTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [ClassData(typeof(AuthenticationDtoTestData))]
    public async Task UcValidateLogin_Test(int expected, AuthenticationDto testObjectInput)
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase(
                "test_database_UcValidateLogin_Test" + testObjectInput.Key)
            .EnableSensitiveDataLogging().Options;
        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var ucGenerateTokenLogin =
            _ucAuthenticationInjection.GetUcValidateLogin(context);
        var result =
            await ucGenerateTokenLogin.Execute(testObjectInput.Key,
                testObjectInput.Password);
        Assert.Equal(expected, result.Code);
    }
}
