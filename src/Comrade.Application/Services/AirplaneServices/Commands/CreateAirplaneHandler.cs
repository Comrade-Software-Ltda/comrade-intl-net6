using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Core.AirplaneCore;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Application.Services.AirplaneServices.Commands;

public class CreateAirplaneHandler : IRequestHandler<AirplaneCreateDto, ISingleResultDto<EntityDto>>
{
    private readonly IMapper _mapper;
    private readonly IUcAirplaneCreate _createAirplane;

    public CreateAirplaneHandler(IMapper mapper, IUcAirplaneCreate createAirplane)
    {
        _mapper = mapper;
        _createAirplane = createAirplane;
    }

    public async Task<ISingleResultDto<EntityDto>> Handle(AirplaneCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<Airplane>(request);

        var result = await _createAirplane.Execute(mappedObject).ConfigureAwait(false);

        return new SingleResultDto<EntityDto>(result);
    }
}