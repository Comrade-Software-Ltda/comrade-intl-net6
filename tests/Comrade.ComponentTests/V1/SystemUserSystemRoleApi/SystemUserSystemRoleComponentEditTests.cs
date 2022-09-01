using System.Net.Http;
using System.Text;
using Comrade.Core.Messages;
using Comrade.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Comrade.ComponentTests.V1.SystemUserSystemRoleApi;

public class SystemUserSystemRoleComponentEditTests : IClassFixture<CustomWebApplicationFactoryFixture>
{
    [Fact]
    public async Task EditSystemRole()
    {
        var fixture = new CustomWebApplicationFactoryFixture();
        var client = fixture.CustomWebApplicationFactory.CreateClient();
        var token = await GenerateFakeToken.Execute(fixture.Mediator);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var systemUserSystemRole = new SystemUserSystemRole
        {
            Id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a4"),
            SystemUserId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
            SystemRoleId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(systemUserSystemRole), Encoding.UTF8);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var actualResponse = await client.PutAsync("/api/v1/system-user-system-role/edit", httpContent).ConfigureAwait(false);
        var actualResponseString = await actualResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.Equal(HttpStatusCode.NoContent, actualResponse.StatusCode);

        using StringReader stringReader = new(actualResponseString);
        using JsonTextReader reader = new(stringReader) { DateParseHandling = DateParseHandling.None };
        var jsonResponse = await JObject.LoadAsync(reader).ConfigureAwait(false);

        Assert.Equal(BusinessMessage.MSG02, jsonResponse["message"]);
    }
}