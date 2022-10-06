using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Application.Components.SystemUserComponent.Contracts;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts
{
    public class SystemUserSystemRoleDto : SystemUserDto
    {
        public SystemUserSystemRoleDto(ICollection<SystemRoleDto> systemRoles)
        {
            SystemRoles = systemRoles;
        }

        public ICollection<SystemRoleDto> SystemRoles { get; set; }
    }
}
