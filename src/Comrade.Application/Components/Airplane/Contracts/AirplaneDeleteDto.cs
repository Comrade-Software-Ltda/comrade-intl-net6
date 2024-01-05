using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.Airplane.Contracts;

public class AirplaneDeleteDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}
