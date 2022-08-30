using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using Comrade.Core.SystemUserSystemRoleCore;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Handlers;

public class SystemUserSystemRoleCreateHandler : IRequestHandler<SystemUserSystemRoleCreateDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemUserSystemRoleCreate _createUc;
    private readonly IMapper _mapper;

    public SystemUserSystemRoleCreateHandler(IMapper mapper, IUcSystemUserSystemRoleCreate createUc)
    {
        _mapper = mapper;
        _createUc = createUc;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserSystemRoleCreateDto request, CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemUserSystemRoleCreateCommand>(request);
        var result = await _createUc.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}