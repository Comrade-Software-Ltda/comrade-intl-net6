using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemMenuComponent.Contracts;

public class SystemMenuDeleteDto : SystemMenuDto, IRequest<SingleResultDto<EntityDto>>
{
}