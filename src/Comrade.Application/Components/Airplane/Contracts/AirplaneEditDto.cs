using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.Airplane.Contracts;

public class AirplaneEditDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}
