using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Services.AirplaneComponent.Dtos;

public class AirplaneCreateDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}