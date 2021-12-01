using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Services.AirplaneServices.Dtos;

public class AirplaneDeleteDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}