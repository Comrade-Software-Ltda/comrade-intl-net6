using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Core.SystemRoleCore;
using Comrade.Core.SystemRoleCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemRoleComponent.Handlers;

public class SystemRoleCreateHandler(IMapper mapper, IUcSystemRoleCreate createUc)
    : IRequestHandler<SystemRoleCreateDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(SystemRoleCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<SystemRoleCreateCommand>(request);
        var result = await createUc.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
