using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Comrade.ComponentTests.V1.AirplaneApi;

public class AirplaneComponentTests : IClassFixture<CustomWebApplicationFactoryFixture>
{
    private readonly CustomWebApplicationFactoryFixture _fixture;

    public AirplaneComponentTests(CustomWebApplicationFactoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetAirplaneReturnsList()
    {
        var client = _fixture
            .CustomWebApplicationFactory
            .CreateClient();

        var token = await GenerateFakeToken.Execute(_fixture.Mediator);

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var actualResponse = await client
            .GetAsync("/api/v1/airplane/get-all")
            .ConfigureAwait(false);

        var actualResponseString = await actualResponse.Content
            .ReadAsStringAsync()
            .ConfigureAwait(false);

        Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);

        using StringReader stringReader = new(actualResponseString);
        using JsonTextReader reader = new(stringReader)
            {DateParseHandling = DateParseHandling.None};
        var jsonResponse = await JObject.LoadAsync(reader)
            .ConfigureAwait(false);

        Assert.Equal(JTokenType.String, jsonResponse["data"]![0]!["model"]!.Type);
        Assert.Equal(JTokenType.Integer, jsonResponse["data"]![0]!["passengerQuantity"]!.Type);

        Assert.True(int.TryParse(
            jsonResponse["data"]![0]!["passengerQuantity"]!.Value<string>(),
            out var _));
    }
}
