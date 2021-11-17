using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Services.SystemMenuServices.Dtos;

public class SystemMenuCreateDto : SystemMenuDto, IRequest<SingleResultDto<EntityDto>>
{
}