﻿using System.Net.Http;
using System.Text;
using Comrade.Core.Messages;
using Comrade.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Comrade.ComponentTests.V1.SystemPermissionApi;

public class SystemPermissionComponentCreateTests : IClassFixture<CustomWebApplicationFactoryFixture>
{
    [Fact]
    public async Task CreateSystemPermission()
    {
        var fixture = new CustomWebApplicationFactoryFixture();
        var client = fixture.CustomWebApplicationFactory.CreateClient();
        var token = await GenerateFakeToken.Execute(fixture.Mediator);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var systemPermission = new SystemPermission
        {
            Name = "ACESSO NOVO",
            Tag = "ACEN"
        };
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(systemPermission), Encoding.UTF8);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var actualResponse =
            await client.PostAsync("/api/v1/system-permission/create", httpContent);
        var actualResponseString = await actualResponse.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Created, actualResponse.StatusCode);

        using StringReader stringReader = new(actualResponseString);
        using JsonTextReader reader = new(stringReader) {DateParseHandling = DateParseHandling.None};
        var jsonResponse = await JObject.LoadAsync(reader);

        Assert.Equal(BusinessMessage.MSG01, jsonResponse["message"]);
    }
}
