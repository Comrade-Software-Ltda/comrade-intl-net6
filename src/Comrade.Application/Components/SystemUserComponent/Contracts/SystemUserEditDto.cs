using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Contracts;

public class SystemUserEditDto : SystemUserDto, IRequest<SingleResultDto<EntityDto>>
{
}
