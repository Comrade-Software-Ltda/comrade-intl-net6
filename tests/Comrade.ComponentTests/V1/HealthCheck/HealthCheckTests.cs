using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Comrade.ComponentTests.V1.HealthCheck;

public class HealthCheckTests : IClassFixture<CustomWebApplicationFactoryFixture>
{
    private readonly CustomWebApplicationFactoryFixture _fixture;

    public HealthCheckTests(CustomWebApplicationFactoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetAccountsReturnsList()
    {
        var client = _fixture
            .CustomWebApplicationFactory
            .CreateClient();

        var actualResponse = await client
            .GetAsync("/hc")
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

        Assert.NotEmpty(jsonResponse);
        Assert.NotNull(jsonResponse);
    }
}
