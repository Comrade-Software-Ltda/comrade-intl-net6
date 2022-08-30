using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;

public class SystemUserSystemRoleEditDto : SystemUserSystemRoleDto, IRequest<SingleResultDto<EntityDto>>
{
}