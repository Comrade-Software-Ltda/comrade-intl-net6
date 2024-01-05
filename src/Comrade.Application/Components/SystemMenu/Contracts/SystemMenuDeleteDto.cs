using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemMenu.Contracts;

public class SystemMenuDeleteDto : IRequest<SingleResultDto<EntityDto>>
{
}
