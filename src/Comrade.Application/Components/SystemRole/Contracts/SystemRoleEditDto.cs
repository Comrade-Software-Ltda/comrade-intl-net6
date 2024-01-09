using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemRole.Contracts;

public class SystemRoleEditDto : SystemRoleDto, IRequest<SingleResultDto<EntityDto>>
{
}
