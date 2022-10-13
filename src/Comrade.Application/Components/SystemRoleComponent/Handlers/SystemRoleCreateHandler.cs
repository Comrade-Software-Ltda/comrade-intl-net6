using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Core.SystemRoleCore;
using Comrade.Core.SystemRoleCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemRoleComponent.Handlers;

public class SystemRoleCreateHandler : IRequestHandler<SystemRoleCreateDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemRoleCreate _createUc;
    private readonly IMapper _mapper;

    public SystemRoleCreateHandler(IMapper mapper, IUcSystemRoleCreate createUc)
    {
        _mapper = mapper;
        _createUc = createUc;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemRoleCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemRoleCreateCommand>(request);
        var result = await _createUc.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}
