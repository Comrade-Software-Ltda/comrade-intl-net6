﻿using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.Core.AirplaneCore;
using MediatR;

namespace Comrade.Application.Components.AirplaneComponent.Commands;

public class AirplaneCommand : IAirplaneCommand
{
    private readonly IUcAirplaneDelete _deleteAirplane;
    private readonly IMediator _mediator;

    public AirplaneCommand(
        IUcAirplaneDelete deleteAirplane,
        IMediator mediator)
    {
        _deleteAirplane = deleteAirplane;
        _mediator = mediator;
    }

    public async Task<ISingleResultDto<EntityDto>> Create(AirplaneCreateDto dto)
    {
        return await _mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(AirplaneEditDto dto)
    {
        return await _mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(Guid id)
    {
        var result = await _deleteAirplane.Execute(id);
        return new SingleResultDto<EntityDto>(result);
    }
}
