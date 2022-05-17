using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Dtos;

public class SystemUserCreateDto : SystemUserDto, IRequest<SingleResultDto<EntityDto>>
{
}