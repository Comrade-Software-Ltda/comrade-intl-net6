using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.Core.AirplaneCore;
using Comrade.Core.AirplaneCore.Commands;
using MediatR;

namespace Comrade.Application.Components.AirplaneComponent.Handlers;

public class
    AirplaneEditServiceHandler : IRequestHandler<AirplaneEditDto, SingleResultDto<EntityDto>>
{
    private readonly IUcAirplaneEdit _editAirplane;
    private readonly IMapper _mapper;

    public AirplaneEditServiceHandler(IMapper mapper, IUcAirplaneEdit editAirplane)
    {
        _mapper = mapper;
        _editAirplane = editAirplane;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(AirplaneEditDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<AirplaneEditCommand>(request);
        var result = await _editAirplane.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}
