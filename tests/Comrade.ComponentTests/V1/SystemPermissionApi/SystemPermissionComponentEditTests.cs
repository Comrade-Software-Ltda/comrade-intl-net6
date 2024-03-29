﻿using System.Net.Http;
using System.Text;
using Comrade.Core.Messages;
using Comrade.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Comrade.ComponentTests.V1.SystemPermissionApi;

public class SystemPermissionComponentEditTests : IClassFixture<CustomWebApplicationFactoryFixture>
{
    [Fact]
    public async Task EditSystemPermission()
    {
        var fixture = new CustomWebApplicationFactoryFixture();
        var client = fixture.CustomWebApplicationFactory.CreateClient();
        var token = await GenerateFakeToken.Execute(fixture.Mediator);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var systemPermission = new SystemPermission
        {
            Id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1"),
            Name = "ACESSO NOVO",
            Tag = "ACEN"
        };
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(systemPermission), Encoding.UTF8);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var actualResponse = await client.PutAsync("/api/v1/system-permission/edit", httpContent);
        var actualResponseString = await actualResponse.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.NoContent, actualResponse.StatusCode);

        using StringReader stringReader = new(actualResponseString);
        using JsonTextReader reader = new(stringReader) {DateParseHandling = DateParseHandling.None};
        var jsonResponse = await JObject.LoadAsync(reader);

        Assert.Equal(BusinessMessage.MSG02, jsonResponse["message"]);
    }
}
