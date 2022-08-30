using Comrade.Application.Bases;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;

public class SystemUserSystemRoleDto : EntityDto
{
    public Guid SystemUserId { get; set; }
    public Guid SystemRoleId { get; set; }
}