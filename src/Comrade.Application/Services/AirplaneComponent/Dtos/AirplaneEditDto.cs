using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Services.AirplaneComponent.Dtos;

public class AirplaneEditDto : AirplaneDto, IRequest<SingleResultDto<EntityDto>>
{
}