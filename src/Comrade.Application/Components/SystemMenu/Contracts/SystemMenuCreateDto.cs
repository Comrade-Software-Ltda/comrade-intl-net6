using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemMenuComponent.Contracts;

public class SystemMenuCreateDto : EntityDto, IRequest<SingleResultDto<EntityDto>>
{
    public Guid? MenuId { get; set; }
    public List<SystemMenuCreateDto>? Submenus { get; set; }
    public string? Title { get; set; }
    public string? Icon { get; set; }
    public string? Description { get; set; }
    public string? Route { get; set; }
}
