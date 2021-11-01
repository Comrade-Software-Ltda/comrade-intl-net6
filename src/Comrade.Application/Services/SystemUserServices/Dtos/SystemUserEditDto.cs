using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Services.SystemUserServices.Dtos;

public class SystemUserEditDto : SystemUserDto, IRequest<SingleResultDto<EntityDto>>
{
}