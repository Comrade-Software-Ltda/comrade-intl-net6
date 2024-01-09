using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRole.Contracts;
using Comrade.Core.SystemRoleCore;
using Comrade.Core.SystemRoleCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemRole.Handlers;

public class SystemRoleEditHandler(IMapper mapper, IUcSystemRoleEdit editUc)
    : IRequestHandler<SystemRoleEditDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(SystemRoleEditDto request, CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<SystemRoleEditCommand>(request);
        var result = await editUc.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
