using Comrade.Application.Components.SystemPermissionComponent.Contracts;

namespace Comrade.Application.Components.SystemUserComponent.Contracts;

public class SystemUserWithPermissionsDto : SystemUserDto
{
    public ICollection<SystemPermissionDto> SystemPermissions { get; set; }
}
