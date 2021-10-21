using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneClaimTests
{
    [Fact]
    public async Task Airplane_Context()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_AirplaneController_Create")
            .EnableSensitiveDataLogging().Options;


        var testObject = new AirplaneCreateDto
        {
            Code = "123",
            Model = "234",
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

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        var airplaneController = AirplaneInjectionController.GetAirplaneController(context);
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

        Assert.Equal(1, context.Airplanes.Count());
    }
}