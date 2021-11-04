using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Services.AirplaneServices.Dtos;

public class AirplaneEditDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}