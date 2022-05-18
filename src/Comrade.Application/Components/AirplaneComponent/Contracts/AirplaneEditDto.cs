using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.AirplaneComponent.Contracts;

public class AirplaneEditDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}