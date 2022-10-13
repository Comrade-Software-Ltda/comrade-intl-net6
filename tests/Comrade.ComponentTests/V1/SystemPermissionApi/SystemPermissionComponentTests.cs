using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Comrade.ComponentTests.V1.SystemPermissionApi;

public class SystemPermissionComponentTests : IClassFixture<CustomWebApplicationFactoryFixture>
{
    [Fact]
    public async Task GetSystemPermissionReturnsList()
    {
        var fixture = new CustomWebApplicationFactoryFixture();
        var client = fixture.CustomWebApplicationFactory.CreateClient();
        var token = await GenerateFakeToken.Execute(fixture.Mediator);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var actualResponse = await client.GetAsync("/api/v1/system-permission/get-all").ConfigureAwait(false);
        var actualResponseString = await actualResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);

        using StringReader stringReader = new(actualResponseString);
        using JsonTextReader reader = new(stringReader) {DateParseHandling = DateParseHandling.None};
        var jsonResponse = await JObject.LoadAsync(reader).ConfigureAwait(false);

        Assert.Equal(JTokenType.String, jsonResponse["data"]![0]!["name"]!.Type);
        Assert.Equal(JTokenType.String, jsonResponse["data"]![0]!["tag"]!.Type);
    }
}
