using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Core.SystemPermissionCore;
using Comrade.Core.SystemPermissionCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemPermissionComponent.Handlers;

public class SystemPermissionEditHandler : IRequestHandler<SystemPermissionEditDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemPermissionEdit _editUc;
    private readonly IMapper _mapper;

    public SystemPermissionEditHandler(IMapper mapper, IUcSystemPermissionEdit editUc)
    {
        _mapper = mapper;
        _editUc = editUc;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemPermissionEditDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemPermissionEditCommand>(request);
        var result = await _editUc.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}
