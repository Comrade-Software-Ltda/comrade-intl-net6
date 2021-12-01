using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Services.AirplaneServices.Dtos;

public class AirplaneCreateDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}