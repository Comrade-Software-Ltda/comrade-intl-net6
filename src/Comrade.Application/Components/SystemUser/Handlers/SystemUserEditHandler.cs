using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUser.Contracts;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemUser.Handlers;

public class SystemUserEditHandler(IMapper mapper, IUcSystemUserEdit editSystemUser)
    : IRequestHandler<SystemUserEditDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserEditDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<SystemUserEditCommand>(request);
        var result = await editSystemUser.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
