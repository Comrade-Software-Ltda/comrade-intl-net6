using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Comrade.ComponentTests.V1.SystemUserSystemRoleApi;

public class SystemUserSystemRoleComponentTests : IClassFixture<CustomWebApplicationFactoryFixture>
{
    [Fact]
    public async Task GetSystemRoleReturnsList()
    {
        var fixture = new CustomWebApplicationFactoryFixture();
        var client = fixture.CustomWebApplicationFactory.CreateClient();
        var token = await GenerateFakeToken.Execute(fixture.Mediator);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var actualResponse = await client.GetAsync("/api/v1/system-user-system-role/get-all").ConfigureAwait(false);
        var actualResponseString = await actualResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);

        using StringReader stringReader = new(actualResponseString);
        using JsonTextReader reader = new(stringReader) {DateParseHandling = DateParseHandling.None};
        var jsonResponse = await JObject.LoadAsync(reader).ConfigureAwait(false);

        Assert.Equal(JTokenType.String, jsonResponse["data"]![0]!["systemUserId"]!.Type);
        Assert.Equal(JTokenType.String, jsonResponse["data"]![0]!["systemRoleId"]!.Type);
    }
}