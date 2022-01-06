using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Services.SystemUserComponent.Dtos;

public class SystemUserEditDto : SystemUserDto, IRequest<SingleResultDto<EntityDto>>
{
}