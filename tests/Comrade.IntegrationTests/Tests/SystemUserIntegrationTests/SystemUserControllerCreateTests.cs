using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUser.Contracts;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public sealed class SystemUserControllerCreateTests(ServiceProviderFixture fixture)
    : IClassFixture<ServiceProviderFixture>
{
    [Fact]
    public async Task SystemUserController_Create()
    {
        var testObject = new SystemUserCreateDto
        {
            Name = "111",
            Email = "777@testObject",
            Registration = "123"
        };

        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(fixture.SqlContextFixture,
                fixture.MongoDbContextFixture,
                fixture.Mediator);

        var result = await systemUserController.Create(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(201, actualResultValue?.Code);
        }
    }


    [Fact]
    public async Task SystemUserController_Create_Error()
    {
        var testObject = new SystemUserCreateDto
        {
            Email = "777@testObject",
            Registration = "123"
        };

        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(fixture.SqlContextFixture,
                fixture.MongoDbContextFixture,
                fixture.Mediator);

        var result = await systemUserController.Create(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }
    }
}
