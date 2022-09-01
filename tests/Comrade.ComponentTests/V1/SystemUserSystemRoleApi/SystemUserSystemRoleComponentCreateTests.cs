using System.Net.Http;
using System.Text;
using Comrade.Core.Messages;
using Comrade.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Comrade.ComponentTests.V1.SystemUserSystemRoleApi;

public class SystemUserSystemRoleComponentCreateTests : IClassFixture<CustomWebApplicationFactoryFixture>
{
    [Fact]
    public async Task CreateSystemRole()
    {
        var fixture = new CustomWebApplicationFactoryFixture();
        var client = fixture.CustomWebApplicationFactory.CreateClient();
        var token = await GenerateFakeToken.Execute(fixture.Mediator);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var systemUserSystemRole = new SystemUserSystemRole
        {
            SystemUserId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
            SystemRoleId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a7")
        };
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(systemUserSystemRole), Encoding.UTF8);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var actualResponse = await client.PostAsync("/api/v1/system-user-system-role/create", httpContent).ConfigureAwait(false);
        var actualResponseString = await actualResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.Equal(HttpStatusCode.Created, actualResponse.StatusCode);

        using StringReader stringReader = new(actualResponseString);
        using JsonTextReader reader = new(stringReader) { DateParseHandling = DateParseHandling.None };
        var jsonResponse = await JObject.LoadAsync(reader).ConfigureAwait(false);

        Assert.Equal(BusinessMessage.MSG01, jsonResponse["message"]);
    }
}