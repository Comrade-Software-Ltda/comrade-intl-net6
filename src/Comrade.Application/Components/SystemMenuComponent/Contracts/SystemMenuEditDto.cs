using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemMenuComponent.Contracts;

public class SystemMenuEditDto : SystemMenuDto, IRequest<SingleResultDto<EntityDto>>
{
}
