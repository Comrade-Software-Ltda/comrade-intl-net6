using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Services.SystemUserComponent.Dtos;

public class SystemUserCreateDto : SystemUserDto, IRequest<SingleResultDto<EntityDto>>
{
}