using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Comrade.ComponentTests.V1.AirplaneApi;

public class AirplaneComponentTests(CustomWebApplicationFactoryFixture fixture)
    : IClassFixture<CustomWebApplicationFactoryFixture>
{
    [Fact]
    public async Task GetAirplaneReturnsList()
    {
        var client = fixture
            .CustomWebApplicationFactory
            .CreateClient();

        var token = await GenerateFakeToken.Execute(fixture.Mediator);

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var actualResponse = await client
                .GetAsync("/api/v1/airplane/get-all")
            ;

        var actualResponseString = await actualResponse.Content
                .ReadAsStringAsync()
            ;

        Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);

        using StringReader stringReader = new(actualResponseString);
        using JsonTextReader reader = new(stringReader)
            {DateParseHandling = DateParseHandling.None};
        var jsonResponse = await JObject.LoadAsync(reader)
            ;

        Assert.Equal(JTokenType.String, jsonResponse["data"]![0]!["model"]!.Type);
        Assert.Equal(JTokenType.Integer, jsonResponse["data"]![0]!["passengerQuantity"]!.Type);

        Assert.True(int.TryParse(
            jsonResponse["data"]![0]!["passengerQuantity"]!.Value<string>(),
            out var _));
    }
}
