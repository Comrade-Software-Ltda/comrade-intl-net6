using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Contracts;

public class SystemUserCreateDto : SystemUserDto, IRequest<SingleResultDto<EntityDto>>
{
}