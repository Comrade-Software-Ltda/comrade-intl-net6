using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using MediatR;

namespace Comrade.Application.Services.AirplaneServices.Dtos;

public class AirplaneEditDto : AirplaneDto, IRequest<ISingleResultDto<EntityDto>>
{
}