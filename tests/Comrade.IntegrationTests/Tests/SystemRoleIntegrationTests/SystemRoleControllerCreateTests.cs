using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Core.Messages;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemRoleTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemRoleIntegrationTests;

public sealed class SystemRoleControllerCreateTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemRoleControllerCreateTests()
    {
        _fixture = new ServiceProviderFixture();
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemRoleController_Create()
    {
        var testObject = new SystemRoleCreateDto
        {
            Name = "ROLE"
        };
        var controller = SystemRoleInjectionController.GetSystemRoleController(_fixture.SqlContextFixture,
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
    public async Task SystemRoleController_Create_NullName_Error()
    {
        var controller = SystemRoleInjectionController.GetSystemRoleController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Create(new SystemRoleCreateDto());
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }
    }

    [Fact]
    public async Task SystemRoleController_Create_DuplicateName_Error()
    {
        var testObject = new SystemRoleCreateDto
        {
            Name = " aDm "
        };
        var controller = SystemRoleInjectionController.GetSystemRoleController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Create(testObject);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
            Assert.Equal(BusinessMessage.MSG10, actualResultValue?.Message);
        }
    }
}
