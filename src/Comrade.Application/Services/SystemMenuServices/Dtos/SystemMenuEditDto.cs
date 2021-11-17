using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Services.SystemMenuServices.Dtos;

public class SystemMenuEditDto : SystemMenuDto, IRequest<SingleResultDto<EntityDto>>
{
}