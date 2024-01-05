using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemPermission.Contracts;

public class SystemPermissionEditDto : SystemPermissionDto, IRequest<SingleResultDto<EntityDto>>
{
}
