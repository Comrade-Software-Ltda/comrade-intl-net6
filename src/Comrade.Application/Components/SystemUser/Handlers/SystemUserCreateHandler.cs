using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUser.Contracts;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemUser.Handlers;

public class
    SystemUserCreateHandler(IMapper mapper, IUcSystemUserCreate createSystemUser)
    : IRequestHandler<SystemUserCreateDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<SystemUserCreateCommand>(request);
        var result = await createSystemUser.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
