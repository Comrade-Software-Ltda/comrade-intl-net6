using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Core.AirplaneCore;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Application.Services.AirplaneServices.Handlers;

public class AirplaneEditHandler : IRequestHandler<AirplaneEditDto, SingleResultDto<EntityDto>>
{
    private readonly IUcAirplaneEdit _editAirplane;
    private readonly IMapper _mapper;

    public AirplaneEditHandler(IMapper mapper, IUcAirplaneEdit editAirplane)
    {
        _mapper = mapper;
        _editAirplane = editAirplane;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(AirplaneEditDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<Airplane>(request);
        var result = await _editAirplane.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}