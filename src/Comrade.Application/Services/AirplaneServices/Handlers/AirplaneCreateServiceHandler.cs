using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Core.AirplaneCore;
using Comrade.Core.AirplaneCore.Commands;
using MediatR;

namespace Comrade.Application.Services.AirplaneServices.Handlers;

public class AirplaneCreateServiceHandler : IRequestHandler<AirplaneCreateDto, ISingleResultDto<EntityDto>>
{
    private readonly IUcAirplaneCreate _createAirplane;
    private readonly IMapper _mapper;

    public AirplaneCreateServiceHandler(IMapper mapper, IUcAirplaneCreate createAirplane)
    {
        _mapper = mapper;
        _createAirplane = createAirplane;
    }

    public async Task<ISingleResultDto<EntityDto>> Handle(AirplaneCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<AirplaneCreateCommand>(request);
        var result = await _createAirplane.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}