using Comrade.Application.Bases;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Core.Messages;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemPermissionTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemPermissionIntegrationTests;

public sealed class SystemPermissionControllerCreateTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemPermissionControllerCreateTests()
    {
        _fixture = new ServiceProviderFixture();
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemPermissionController_Create()
    {
        const string name = "ACESSO NOVO";
        const string tag = "ACEN";
        var testObject = new SystemPermissionCreateDto
        {
            Name = name,
            Tag = tag
        };
        var controller = SystemPermissionInjectionController.GetSystemPermissionController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Create(testObject);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(201, actualResultValue?.Code);
        }
    }

    [Fact]
    public async Task SystemPermissionController_Create_NullName_Error()
    {
        var testObject = new SystemPermissionCreateDto
        {
            Tag = "ACEN"
        };
        var controller = SystemPermissionInjectionController.GetSystemPermissionController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Create(testObject);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }
    }

    [Fact]
    public async Task SystemPermissionController_Create_NullTag_Error()
    {
        var testObject = new SystemPermissionCreateDto
        {
            Name = "ACESSO NOVO"
        };
        var controller = SystemPermissionInjectionController.GetSystemPermissionController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Create(testObject);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }
    }

    [Fact]
    public async Task SystemPermissionController_Create_DuplicateTag_Error()
    {
        var testObject = new SystemPermissionCreateDto
        {
            Name = "ACESSO",
            Tag = "  ace  "
        };
        var controller = SystemPermissionInjectionController.GetSystemPermissionController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Create(testObject);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
            Assert.Equal(BusinessMessage.MSG11, actualResultValue?.Message);
        }
    }
}
