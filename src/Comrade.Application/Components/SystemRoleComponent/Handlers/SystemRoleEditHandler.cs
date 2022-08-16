using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Core.SystemRoleCore;
using Comrade.Core.SystemRoleCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemRoleComponent.Handlers;

public class SystemRoleEditHandler : IRequestHandler<SystemRoleEditDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemRoleEdit _editUc;
    private readonly IMapper _mapper;

    public SystemRoleEditHandler(IMapper mapper, IUcSystemRoleEdit editUc)
    {
        _mapper = mapper;
        _editUc = editUc;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemRoleEditDto request, CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemRoleEditCommand>(request);
        var result = await _editUc.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}
