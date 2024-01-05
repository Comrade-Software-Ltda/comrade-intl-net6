using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemMenuComponent.Contracts;
using Comrade.Core.SystemMenuCore;
using Comrade.Core.SystemMenuCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemMenuComponent.Handlers;

public class
    SystemMenuCreateHandler(IMapper mapper, IUcSystemMenuCreate createSystemMenu)
    : IRequestHandler<SystemMenuCreateDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(SystemMenuCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<SystemMenuCreateCommand>(request);
        var result = await createSystemMenu.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
