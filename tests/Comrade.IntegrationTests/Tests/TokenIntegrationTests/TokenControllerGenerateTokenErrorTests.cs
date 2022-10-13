using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.AuthenticationComponent.Contracts;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.TokenIntegrationTests;

public sealed class TokenControllerGenerateTokenErrorTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public TokenControllerGenerateTokenErrorTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task TokenController_GenerateToken_Error()
    {
        var testObject = new AuthenticationDto
        {
            Key = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
            Password = "Error"
        };

        var tokenController =
            TokenInjectionController.GetTokenController(_fixture.SqlContextFixture,
                _fixture.Mediator);
        var result = await tokenController.GenerateToken(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<UserDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(1001, actualResultValue?.Code);
        }
    }
}
