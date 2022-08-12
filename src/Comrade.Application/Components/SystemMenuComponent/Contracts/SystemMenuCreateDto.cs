using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemMenuComponent.Contracts;

public class SystemMenuCreateDto : SystemMenuDto, IRequest<SingleResultDto<EntityDto>>
{
}