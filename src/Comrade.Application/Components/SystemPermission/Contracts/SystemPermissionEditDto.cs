using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemPermissionComponent.Contracts;

public class SystemPermissionEditDto : SystemPermissionDto, IRequest<SingleResultDto<EntityDto>>
{
}
