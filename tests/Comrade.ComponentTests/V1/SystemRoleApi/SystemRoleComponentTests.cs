using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Comrade.ComponentTests.V1.SystemRoleApi;

public class SystemRoleComponentTests(CustomWebApplicationFactoryFixture fixture)
    : IClassFixture<CustomWebApplicationFactoryFixture>
{
    [Fact]
    public async Task GetSystemRoleReturnsList()
    {
        var client = fixture.CustomWebApplicationFactory.CreateClient();
        var token = await GenerateFakeToken.Execute(fixture.Mediator);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var actualResponse = await client
                .GetAsync("/api/v1/system-role/get-all")
            ;

        var actualResponseString = await actualResponse.Content
                .ReadAsStringAsync()
            ;

        Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);

        using StringReader stringReader = new(actualResponseString);
        using JsonTextReader reader = new(stringReader) {DateParseHandling = DateParseHandling.None};
        var jsonResponse = await JObject.LoadAsync(reader);

        Assert.Equal(JTokenType.String, jsonResponse["data"]![0]!["name"]!.Type);
    }
}
