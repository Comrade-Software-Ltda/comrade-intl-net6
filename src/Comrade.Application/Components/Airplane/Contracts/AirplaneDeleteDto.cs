using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.AirplaneComponent.Contracts;

public class AirplaneDeleteDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}
