using Comrade.Application.Bases;

namespace Comrade.Application.Components.SystemMenuComponent.Contracts;

public class SystemMenuSimpleDto : EntityDto
{
    public Guid? MenuId { get; set; }
    public string? Text { get; set; }
    public string? Description { get; set; }
    public string? Route { get; set; }
}