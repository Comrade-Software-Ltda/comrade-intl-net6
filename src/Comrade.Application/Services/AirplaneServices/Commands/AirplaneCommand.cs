using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Core.AirplaneCore;
using MediatR;

namespace Comrade.Application.Services.AirplaneServices.Commands;

public class AirplaneCommand : IAirplaneCommand
{
    private readonly IUcAirplaneDelete _deleteAirplane;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AirplaneCommand(
        IUcAirplaneDelete deleteAirplane,
        IMapper mapper, IMediator mediator)
    {
        _deleteAirplane = deleteAirplane;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ISingleResultDto<EntityDto>> Create(AirplaneCreateDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(AirplaneEditDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(int id)
    {
        var result = await _deleteAirplane.Execute(id).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}