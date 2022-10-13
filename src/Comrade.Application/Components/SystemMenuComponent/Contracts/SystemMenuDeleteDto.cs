using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemMenuComponent.Contracts;

public class SystemMenuDeleteDto : IRequest<SingleResultDto<EntityDto>>
{
}
