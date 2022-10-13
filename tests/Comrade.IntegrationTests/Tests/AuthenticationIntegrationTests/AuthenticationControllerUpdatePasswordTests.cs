using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.AuthenticationComponent.Contracts;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AuthenticationIntegrationTests;

public sealed class
    AuthenticationControllerUpdatePasswordTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public AuthenticationControllerUpdatePasswordTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task AuthenticationController_UpdatePassword()
    {
        var testObject = new AuthenticationDto
        {
            Key = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
            Password = "123456"
        };

        var authenticationController =
            AuthenticationInjectionController.GetAuthenticationController(
                _fixture.SqlContextFixture, _fixture.Mediator);
        var result = await authenticationController.UpdatePassword(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
        }
    }
}
