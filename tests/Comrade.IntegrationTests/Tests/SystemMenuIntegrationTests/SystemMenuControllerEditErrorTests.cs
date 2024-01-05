using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenu.Contracts;
using Comrade.UnitTests.Tests.SystemMenuTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public sealed class SystemMenuControllerEditErrorTests(ServiceProviderFixture fixture)
    : IClassFixture<ServiceProviderFixture>
{
    [Fact]
    public async Task SystemMenuController_Edit_Error()
    {
        var systemMenuId = Guid.NewGuid();
        const string systemMenuTitle = "Main Teste Edit";
        const string systemMenuDescription = "Description Main Teste Edit";
        const string systemMenuRoute = "/teste";

        var testObject = new SystemMenuEditDto
        {
            Id = systemMenuId,
            Title = systemMenuTitle,
            Description = systemMenuDescription,
            Route = systemMenuRoute
        };

        var systemMenuController =
            SystemMenuInjectionController.GetSystemMenuController(fixture.SqlContextFixture,
                fixture.MongoDbContextFixture,
                fixture.Mediator);

        var result = await systemMenuController.Edit(testObject);

        if (result is ObjectResult objectResult)
        {
            var actualResultValue = objectResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(404, actualResultValue?.Code);
        }
    }
}
