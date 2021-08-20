#region

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using Xunit;

#endregion

namespace Comrade.ComponentTests.V1.AirplaneApi;

[Collection("Api Collection")]
public class AirplaneComponentTests
{
    private readonly CustomWebApplicationFactoryFixture _fixture;

    public AirplaneComponentTests(CustomWebApplicationFactoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetAccountsReturnsList()
    {
        HttpClient client = _fixture
            .CustomWebApplicationFactory
            .CreateClient();

        var token = GenerateFakeToken.Execute();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage actualResponse = await client
            .GetAsync("/api/v1/airplane/get-all")
            .ConfigureAwait(false);

        string actualResponseString = await actualResponse.Content
            .ReadAsStringAsync()
            .ConfigureAwait(false);

        Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);

        using StringReader stringReader = new(actualResponseString);
        using JsonTextReader reader = new(stringReader)
        { DateParseHandling = DateParseHandling.None };
        JObject jsonResponse = await JObject.LoadAsync(reader)
            .ConfigureAwait(false);

        Assert.Equal(JTokenType.String, jsonResponse["data"]![0]!["model"]!.Type);
        Assert.Equal(JTokenType.Integer, jsonResponse["data"]![0]!["passengerQuantity"]!.Type);

        Assert.True(int.TryParse(
            jsonResponse["data"]![0]!["passengerQuantity"]!.Value<string>(),
            out var _));
    }
}
