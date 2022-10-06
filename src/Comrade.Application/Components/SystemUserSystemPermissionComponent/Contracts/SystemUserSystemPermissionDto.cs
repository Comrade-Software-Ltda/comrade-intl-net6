using Comrade.Application.Bases;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Application.Components.SystemUserComponent.Contracts;

namespace Comrade.Application.Components.SystemUserSystemPermissionComponent.Contracts
{
    public class SystemUserSystemPermissionDto : SystemUserDto
    {
        public SystemUserSystemPermissionDto(ICollection<SystemPermissionDto> systemPermissions)
        {
            SystemPermissions = systemPermissions;
        }
        public ICollection<SystemPermissionDto> SystemPermissions { get; set; }
    }
}
