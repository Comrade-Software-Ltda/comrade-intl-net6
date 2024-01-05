using Comrade.Application.Bases;

namespace Comrade.Application.Components.SystemRole.Contracts;

public class SystemRoleDto : EntityDto
{
    public string? Name { get; set; }
    public string? Tag { get; set; }
}
