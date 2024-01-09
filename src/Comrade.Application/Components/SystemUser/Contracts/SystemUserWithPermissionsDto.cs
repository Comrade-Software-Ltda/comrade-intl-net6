using Comrade.Application.Components.SystemPermission.Contracts;

namespace Comrade.Application.Components.SystemUser.Contracts;

public class SystemUserWithPermissionsDto : SystemUserDto
{
    public ICollection<SystemPermissionDto> SystemPermissions { get; set; }
}
