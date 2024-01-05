using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Core.SystemMenuCore;
using Comrade.Core.SystemMenuCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemMenuComponent.Handlers;

public class SystemMenuEditHandler(IMapper mapper, IUcSystemMenuEdit editSystemMenu)
    : IRequestHandler<SystemMenuEditDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(SystemMenuEditDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<SystemMenuEditCommand>(request);
        var result = await editSystemMenu.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
