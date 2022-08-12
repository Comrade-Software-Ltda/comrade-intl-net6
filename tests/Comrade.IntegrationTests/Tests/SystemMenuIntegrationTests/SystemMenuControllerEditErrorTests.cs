using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerEditErrorTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemMenuControllerEditErrorTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SystemMenuController_Edit_Error()
    {
        var systemMenuId = Guid.NewGuid();
        const string systemMenuText = "Main Teste Edit";
        const string systemMenuDescription = "Description Main Teste Edit";
        const string systemMenuRoute = "/teste";

        var testObject = new SystemMenuEditDto
        {
            Id = systemMenuId,
            Text = systemMenuText,
            Description = systemMenuDescription,
            Route = systemMenuRoute
        };

        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);

        var result = await systemMenuController.Edit(testObject);

        if (result is ObjectResult objectResult)
        {
            var actualResultValue = objectResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(404, actualResultValue?.Code);
        }
    }
}