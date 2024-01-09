using Comrade.Application.Components.SystemRole.Contracts;

namespace Comrade.Application.Components.SystemUser.Contracts;

public class SystemUserWithRolesDto : SystemUserDto
{
    public ICollection<SystemRoleDto> SystemRoles { get; set; }
}
