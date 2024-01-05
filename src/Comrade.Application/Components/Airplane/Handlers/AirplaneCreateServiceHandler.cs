using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.Airplane.Contracts;
using Comrade.Core.AirplaneCore;
using Comrade.Core.AirplaneCore.Commands;
using MediatR;

namespace Comrade.Application.Components.Airplane.Handlers;

public class
    AirplaneCreateServiceHandler(IMapper mapper, IUcAirplaneCreate createAirplane)
    : IRequestHandler<AirplaneCreateDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(AirplaneCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<AirplaneCreateCommand>(request);
        var result = await createAirplane.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
