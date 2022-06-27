using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Handlers;

public class
    SystemUserCreateHandler : IRequestHandler<SystemUserCreateDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemUserCreate _createSystemUser;
    private readonly IMapper _mapper;

    public SystemUserCreateHandler(IMapper mapper, IUcSystemUserCreate createSystemUser)
    {
        _mapper = mapper;
        _createSystemUser = createSystemUser;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserCreateDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemUserCreateCommand>(request);
        var result = await _createSystemUser.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}