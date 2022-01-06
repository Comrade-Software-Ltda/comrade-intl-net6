using System;
using Comrade.Application.Bases;
using Comrade.Application.Services.AuthenticationComponent.Dtos;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.TokenIntegrationTests;

public sealed class TokenControllerGenerateTokenTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public TokenControllerGenerateTokenTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task TokenController_GenerateToken()
    {
        var testObject = new AuthenticationDto
        {
            Key = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
            Password = "123456"
        };

        var tokenController =
            TokenInjectionController.GetTokenController(_fixture.SqlContextFixture,
                _fixture.Mediator);
        var result = await tokenController.GenerateToken(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<UserDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
        }
    }
}