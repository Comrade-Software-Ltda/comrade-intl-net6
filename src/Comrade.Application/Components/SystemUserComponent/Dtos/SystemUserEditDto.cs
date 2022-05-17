using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Dtos;

public class SystemUserEditDto : SystemUserDto, IRequest<SingleResultDto<EntityDto>>
{
}