using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemPermission.Contracts;
using Comrade.Core.SystemPermissionCore;
using Comrade.Core.SystemPermissionCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemPermission.Handlers;

public class SystemPermissionCreateHandler(IMapper mapper, IUcSystemPermissionCreate createUc)
    : IRequestHandler<SystemPermissionCreateDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(SystemPermissionCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<SystemPermissionCreateCommand>(request);
        var result = await createUc.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
