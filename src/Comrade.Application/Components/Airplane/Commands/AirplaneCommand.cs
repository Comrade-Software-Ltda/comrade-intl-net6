using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.Airplane.Contracts;
using Comrade.Core.AirplaneCore;
using MediatR;

namespace Comrade.Application.Components.Airplane.Commands;

public class AirplaneCommand(
    IUcAirplaneDelete deleteAirplane,
    IMediator mediator) : IAirplaneCommand
{
    public async Task<ISingleResultDto<EntityDto>> Create(AirplaneCreateDto dto)
    {
        return await mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(AirplaneEditDto dto)
    {
        return await mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(Guid id)
    {
        var result = await deleteAirplane.Execute(id);
        return new SingleResultDto<EntityDto>(result);
    }
}
