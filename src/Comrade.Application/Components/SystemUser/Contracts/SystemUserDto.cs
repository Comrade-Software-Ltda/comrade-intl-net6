using Comrade.Application.Bases;

namespace Comrade.Application.Components.SystemUserComponent.Contracts;

public class SystemUserDto : EntityDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Registration { get; set; }
    public DateTime? RegisterDate { get; set; }
}
