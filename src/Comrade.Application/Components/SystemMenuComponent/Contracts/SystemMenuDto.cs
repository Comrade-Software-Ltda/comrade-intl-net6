using Comrade.Application.Bases;

namespace Comrade.Application.Components.SystemMenuComponent.Contracts;

public class SystemMenuDto : EntityDto
{
    public Guid? FatherId { get; set; }
    public string? Text { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }
    public string? Route { get; set; }
}