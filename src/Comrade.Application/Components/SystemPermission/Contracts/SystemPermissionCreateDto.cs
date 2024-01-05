using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemPermission.Contracts;

public class SystemPermissionCreateDto : SystemPermissionDto, IRequest<SingleResultDto<EntityDto>>
{
}
