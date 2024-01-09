using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.Airplane.Contracts;

public class AirplaneCreateDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}
