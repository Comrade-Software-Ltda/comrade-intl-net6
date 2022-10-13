using System.Net.Http;
using System.Text;
using Comrade.Core.Messages;
using Comrade.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Comrade.ComponentTests.V1.SystemPermissionApi;

public class SystemPermissionComponentEditErrorTests : IClassFixture<CustomWebApplicationFactoryFixture>
{
    [Fact]
    public async Task EditSystemPermissionError()
    {
        var fixture = new CustomWebApplicationFactoryFixture();
        var client = fixture.CustomWebApplicationFactory.CreateClient();
        var token = await GenerateFakeToken.Execute(fixture.Mediator);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var systemPermission = new SystemPermission
        {
            Id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1"),
            Name = "ACESSO",
            Tag = "  ace  "
        };
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(systemPermission), Encoding.UTF8);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var actualResponse = await client.PutAsync("/api/v1/system-permission/edit", httpContent).ConfigureAwait(false);
        var actualResponseString = await actualResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.Equal(HttpStatusCode.Conflict, actualResponse.StatusCode);

        using StringReader stringReader = new(actualResponseString);
        using JsonTextReader reader = new(stringReader) {DateParseHandling = DateParseHandling.None};
        var jsonResponse = await JObject.LoadAsync(reader).ConfigureAwait(false);

        Assert.Equal(BusinessMessage.MSG11, jsonResponse["message"]);
    }
}
