using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Core.SystemPermissionCore;
using Comrade.Core.SystemPermissionCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemPermissionComponent.Handlers;

public class SystemPermissionEditHandler(IMapper mapper, IUcSystemPermissionEdit editUc)
    : IRequestHandler<SystemPermissionEditDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(SystemPermissionEditDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<SystemPermissionEditCommand>(request);
        var result = await editUc.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
