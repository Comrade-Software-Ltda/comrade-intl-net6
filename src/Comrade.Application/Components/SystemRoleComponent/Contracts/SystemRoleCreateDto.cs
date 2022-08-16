using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemRoleComponent.Contracts;

public class SystemRoleCreateDto : SystemRoleDto, IRequest<SingleResultDto<EntityDto>>
{
}
