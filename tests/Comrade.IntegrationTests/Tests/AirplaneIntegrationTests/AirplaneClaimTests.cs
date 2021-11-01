using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneClaimTests : IClassFixture<ServiceProviderFixture>
{
    readonly ServiceProviderFixture _fixture;

    public AirplaneClaimTests(ServiceProviderFixture fixture)
    {
        this._fixture = fixture;
    }


    [Fact]
    public async Task Airplane_Claim()
    {
        var sp = _fixture.InitiateConxtext("Airplane_Claim");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;
        var airplaneController = AirplaneInjectionController.GetAirplaneController(context, mediator);

        var testObject = new AirplaneCreateDto
        {
            Code = "444",
            Model = "585",
            PassengerQuantity = 456
        };

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "username"),
            new(ClaimTypes.NameIdentifier, "userId"),
            new("Name", "John Doe"),
            new("Key", "1")
        };

        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        airplaneController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claimsPrincipal }
        };

        var result = await airplaneController.Create(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(201, actualResultValue?.Code);
        }
    }
}