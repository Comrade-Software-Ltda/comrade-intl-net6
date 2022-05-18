using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Core.SystemUserCore;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Commands;

public class SystemUserCommand : ISystemUserCommand
{
    private readonly IUcSystemUserDelete _deleteSystemUser;
    private readonly IMediator _mediator;

    public SystemUserCommand(
        IUcSystemUserDelete deleteSystemUser, IMediator mediator)
    {
        _deleteSystemUser = deleteSystemUser;
        _mediator = mediator;
    }

    public async Task<ISingleResultDto<EntityDto>> Create(SystemUserCreateDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(SystemUserEditDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(Guid id)
    {
        var result = await _deleteSystemUser.Execute(id).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}