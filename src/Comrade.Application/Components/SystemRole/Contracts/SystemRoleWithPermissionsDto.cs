using Comrade.Application.Components.SystemPermission.Contracts;

namespace Comrade.Application.Components.SystemRole.Contracts;

public class SystemRoleWithPermissionsDto : SystemRoleDto
{
    public ICollection<SystemPermissionDto> SystemPermissions { get; set; }
}
