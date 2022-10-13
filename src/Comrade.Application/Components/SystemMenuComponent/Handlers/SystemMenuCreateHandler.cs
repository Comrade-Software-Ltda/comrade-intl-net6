using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Core.SystemMenuCore;
using Comrade.Core.SystemMenuCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemMenuComponent.Handlers;

public class
    SystemMenuCreateHandler : IRequestHandler<SystemMenuCreateDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemMenuCreate _createSystemMenu;
    private readonly IMapper _mapper;

    public SystemMenuCreateHandler(IMapper mapper, IUcSystemMenuCreate createSystemMenu)
    {
        _mapper = mapper;
        _createSystemMenu = createSystemMenu;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemMenuCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemMenuCreateCommand>(request);
        var result = await _createSystemMenu.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}
