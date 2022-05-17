using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.AirplaneComponent.Dtos;

public class AirplaneDeleteDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}