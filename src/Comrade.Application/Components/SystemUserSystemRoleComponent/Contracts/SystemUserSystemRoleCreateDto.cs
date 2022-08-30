using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;

public class SystemUserSystemRoleCreateDto : SystemUserSystemRoleDto, IRequest<SingleResultDto<EntityDto>>
{
}