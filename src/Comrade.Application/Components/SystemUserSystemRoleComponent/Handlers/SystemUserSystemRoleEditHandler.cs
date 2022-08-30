using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using Comrade.Core.SystemUserSystemRoleCore;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Handlers;

public class SystemUserSystemRoleEditHandler : IRequestHandler<SystemUserSystemRoleEditDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemUserSystemRoleEdit _editUc;
    private readonly IMapper _mapper;

    public SystemUserSystemRoleEditHandler(IMapper mapper, IUcSystemUserSystemRoleEdit editUc)
    {
        _mapper = mapper;
        _editUc = editUc;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserSystemRoleEditDto request, CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemUserSystemRoleEditCommand>(request);
        var result = await _editUc.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}