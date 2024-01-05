using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.Core.AirplaneCore;
using Comrade.Core.AirplaneCore.Commands;
using MediatR;

namespace Comrade.Application.Components.AirplaneComponent.Handlers;

public class
    AirplaneEditServiceHandler(IMapper mapper, IUcAirplaneEdit editAirplane)
    : IRequestHandler<AirplaneEditDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(AirplaneEditDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<AirplaneEditCommand>(request);
        var result = await editAirplane.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
