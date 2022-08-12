using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Core.SystemMenuCore;
using Comrade.Core.SystemMenuCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemMenuComponent.Handlers;

public class SystemMenuEditHandler : IRequestHandler<SystemMenuEditDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemMenuEdit _editSystemMenu;
    private readonly IMapper _mapper;

    public SystemMenuEditHandler(IMapper mapper, IUcSystemMenuEdit editSystemMenu)
    {
        _mapper = mapper;
        _editSystemMenu = editSystemMenu;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemMenuEditDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemMenuEditCommand>(request);
        var result = await _editSystemMenu.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}