using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemPermissionComponent.Contracts;

public class SystemPermissionCreateDto : SystemPermissionDto, IRequest<SingleResultDto<EntityDto>>
{
}
