using Comrade.Application.Components.SystemPermissionComponent.Contracts;

namespace Comrade.Application.Components.SystemRoleComponent.Contracts;

public class SystemRoleWithPermissionsDto : SystemRoleDto
{
    public ICollection<SystemPermissionDto> SystemPermissions { get; set; }
}
