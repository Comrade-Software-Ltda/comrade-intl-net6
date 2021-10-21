using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Application.Services.AirplaneServices.Validations;
using Comrade.Core.AirplaneCore;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Application.Services.AirplaneServices.Commands;

public class AirplaneCommand : Service, IAirplaneCommand
{
    private readonly IUcAirplaneCreate _createAirplane;
    private readonly IUcAirplaneDelete _deleteAirplane;
    private readonly IUcAirplaneEdit _editAirplane;
    private readonly IMediator _mediator;

    public AirplaneCommand(IUcAirplaneEdit editAirplane,
        IUcAirplaneCreate createAirplane,
        IUcAirplaneDelete deleteAirplane,
        IMapper mapper, IMediator mediator)
        : base(mapper)
    {
        _editAirplane = editAirplane;
        _createAirplane = createAirplane;
        _deleteAirplane = deleteAirplane;
        _mediator = mediator;
    }

    public async Task<ISingleResultDto<EntityDto>> Create(AirplaneCreateDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(AirplaneEditDto dto)
    {
        var validator =
            await new AirplaneEditValidation().ValidateAsync(dto).ConfigureAwait(false);

        if (!validator.IsValid)
        {
            return new SingleResultDto<EntityDto>(validator);
        }

        var mappedObject = Mapper.Map<Airplane>(dto);

        var result = await _editAirplane.Execute(mappedObject).ConfigureAwait(false);

        var resultDto = new SingleResultDto<EntityDto>(result);

        return resultDto;
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(int id)
    {
        var result = await _deleteAirplane.Execute(id).ConfigureAwait(false);

        var resultDto = new SingleResultDto<EntityDto>(result);

        return resultDto;
    }
}