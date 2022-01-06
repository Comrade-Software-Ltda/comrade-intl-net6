using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Services.AirplaneComponent.Dtos;

public class AirplaneDeleteDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}