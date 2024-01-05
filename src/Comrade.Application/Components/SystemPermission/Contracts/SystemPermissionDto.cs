using Comrade.Application.Bases;

namespace Comrade.Application.Components.SystemPermission.Contracts;

public class SystemPermissionDto : EntityDto
{
    public string? Name { get; set; }
    public string? Tag { get; set; }
}
