using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.AirplaneComponent.Contracts;

public class AirplaneCreateDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}
