using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.AirplaneComponent.Dtos;

public class AirplaneCreateDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}