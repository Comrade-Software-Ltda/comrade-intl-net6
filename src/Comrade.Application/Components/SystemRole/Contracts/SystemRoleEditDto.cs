using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemRoleComponent.Contracts;

public class SystemRoleEditDto : SystemRoleDto, IRequest<SingleResultDto<EntityDto>>
{
}
