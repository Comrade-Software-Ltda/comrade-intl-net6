using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemRole.Contracts;

public class SystemRoleCreateDto : SystemRoleDto, IRequest<SingleResultDto<EntityDto>>
{
}
