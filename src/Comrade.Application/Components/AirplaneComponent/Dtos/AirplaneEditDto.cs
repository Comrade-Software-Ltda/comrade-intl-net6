using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.AirplaneComponent.Dtos;

public class AirplaneEditDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}