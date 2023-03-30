using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemRoleTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemRoleIntegrationTests;

public class SystemRoleControllerManagePermissions : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemRoleControllerManagePermissions(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemRoleController_ManagePermissions()
    {
        var body = CreateManagePermissionsBody();

        var controller = SystemRoleInjectionController.GetSystemRoleController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);

        var result = await controller.ManagePermissions(body);

        IsResultValid(result);

        var quantitySystemRole = QuantitySystemRolePermissionsBySystemRoleId(body.Id);
        Assert.Equal(1, quantitySystemRole);

        var quantitySystemRolePermission =
            QuantitySystemRolePermissionsBySystemPermissionId(body.SystemPermissionIds.FirstOrDefault());
        Assert.Equal(1, quantitySystemRolePermission);
    }

    private int QuantitySystemRolePermissionsBySystemPermissionId(Guid systemPermissionId)
    {
        return _fixture.SqlContextFixture.SystemRolePermissions
            .Count(systemRolePermissions =>
                systemRolePermissions.SystemPermissionId == systemPermissionId);
    }

    private int QuantitySystemRolePermissionsBySystemRoleId(Guid systemRoleId)
    {
        return _fixture.SqlContextFixture.SystemRolePermissions
            .Count(systemRolePermissions =>
                systemRolePermissions.SystemRoleId == systemRoleId);
    }

    private static void IsResultValid(IActionResult result)
    {
        var okResult = result as ObjectResult;
        Assert.NotNull(okResult);

        var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
        Assert.NotNull(actualResultValue);
        Assert.Equal(201, actualResultValue.Code);
    }

    private static SystemRoleManagePermissionsDto CreateManagePermissionsBody()
    {
        return new SystemRoleManagePermissionsDto
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            SystemPermissionIds = new List<Guid> {Guid.Parse("6adf10d0-1b83-46f2-91eb-0c64f1c638a1")}
        };
    }
}
