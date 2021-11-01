using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Core.SystemUserCore;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Application.Services.SystemUserServices.Handlers;

public class SystemUserEditHandler : IRequestHandler<SystemUserEditDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemUserEdit _editSystemUser;
    private readonly IMapper _mapper;

    public SystemUserEditHandler(IMapper mapper, IUcSystemUserEdit editSystemUser)
    {
        _mapper = mapper;
        _editSystemUser = editSystemUser;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserEditDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemUser>(request);
        var result = await _editSystemUser.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}