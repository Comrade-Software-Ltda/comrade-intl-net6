using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Core.SystemPermissionCore;
using Comrade.Core.SystemPermissionCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemPermissionComponent.Handlers;

public class SystemPermissionCreateHandler : IRequestHandler<SystemPermissionCreateDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemPermissionCreate _createUc;
    private readonly IMapper _mapper;

    public SystemPermissionCreateHandler(IMapper mapper, IUcSystemPermissionCreate createUc)
    {
        _mapper = mapper;
        _createUc = createUc;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemPermissionCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemPermissionCreateCommand>(request);
        var result = await _createUc.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}
