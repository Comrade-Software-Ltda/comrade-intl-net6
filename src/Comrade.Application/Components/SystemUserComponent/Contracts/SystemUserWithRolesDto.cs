using Comrade.Application.Components.SystemRoleComponent.Contracts;

namespace Comrade.Application.Components.SystemUserComponent.Contracts;

public class SystemUserWithRolesDto : SystemUserDto
{
    public ICollection<SystemRoleDto> SystemRoles { get; set; }
}
