using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Core.AirplaneCore;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Application.Services.AirplaneServices.Handlers;

public class AirplaneCreateHandler : IRequestHandler<AirplaneCreateDto, SingleResultDto<EntityDto>>
{
    private readonly IMapper _mapper;
    private readonly IUcAirplaneCreate _createAirplane;

    public AirplaneCreateHandler(IMapper mapper, IUcAirplaneCreate createAirplane)
    {
        _mapper = mapper;
        _createAirplane = createAirplane;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(AirplaneCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<Airplane>(request);
        var result = await _createAirplane.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}