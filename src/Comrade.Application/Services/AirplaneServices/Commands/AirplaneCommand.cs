using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Application.Services.AirplaneServices.Validations;
using Comrade.Core.AirplaneCore;
using Comrade.Domain.Models;

namespace Comrade.Application.Services.AirplaneServices.Commands;

public class AirplaneCommand : Service, IAirplaneCommand
{
    private readonly IUcAirplaneCreate _createAirplane;
    private readonly IUcAirplaneDelete _deleteAirplane;
    private readonly IUcAirplaneEdit _editAirplane;

    public AirplaneCommand(IUcAirplaneEdit editAirplane,
        IUcAirplaneCreate createAirplane,
        IUcAirplaneDelete deleteAirplane,
        IMapper mapper)
        : base(mapper)
    {
        _editAirplane = editAirplane;
        _createAirplane = createAirplane;
        _deleteAirplane = deleteAirplane;
    }

    public async Task<ISingleResultDto<AirplaneDto>> Create(AirplaneCreateDto dto)
    {
        var validator = await new AirplaneCreateValidation().ValidateAsync(dto)
            .ConfigureAwait(false);

        if (!validator.IsValid)
        {
            return new SingleResultDto<AirplaneDto>(validator);
        }

        var mappedObject = Mapper.Map<Airplane>(dto);

        var result = await _createAirplane.Execute(mappedObject).ConfigureAwait(false);

        var resultDto = new SingleResultDto<AirplaneDto>(result);

        return resultDto;
    }

    public async Task<ISingleResultDto<AirplaneDto>> Edit(AirplaneEditDto dto)
    {
        var validator =
            await new AirplaneEditValidation().ValidateAsync(dto).ConfigureAwait(false);

        if (!validator.IsValid)
        {
            return new SingleResultDto<AirplaneDto>(validator);
        }

        var mappedObject = Mapper.Map<Airplane>(dto);

        var result = await _editAirplane.Execute(mappedObject).ConfigureAwait(false);

        var resultDto = new SingleResultDto<AirplaneDto>(result);

        return resultDto;
    }

    public async Task<ISingleResultDto<AirplaneDto>> Delete(int id)
    {
        var result = await _deleteAirplane.Execute(id).ConfigureAwait(false);

        var resultDto = new SingleResultDto<AirplaneDto>(result);

        return resultDto;
    }
}