#region

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using Xunit;

#endregion

namespace Comrade.ComponentTests.V1.HealthCheck;

[Collection("Api Collection")]
public class HealthCheckTests
{
    private readonly CustomWebApplicationFactoryFixture _fixture;

    public HealthCheckTests(CustomWebApplicationFactoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetAccountsReturnsList()
    {
        HttpClient client = _fixture
            .CustomWebApplicationFactory
            .CreateClient();

        HttpResponseMessage actualResponse = await client
            .GetAsync("/health")
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

        Assert.NotEmpty(jsonResponse);
        Assert.NotNull(jsonResponse);
    }
}
