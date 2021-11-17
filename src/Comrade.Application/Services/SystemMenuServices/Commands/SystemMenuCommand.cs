using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.SystemMenuServices.Dtos;
using Comrade.Core.SystemMenuCore;
using MediatR;

namespace Comrade.Application.Services.SystemMenuServices.Commands;

public class SystemMenuCommand : ISystemMenuCommand
{
    private readonly IUcSystemMenuDelete _deleteSystemMenu;
    private readonly IMediator _mediator;

    public SystemMenuCommand(
        IUcSystemMenuDelete deleteSystemMenu,
        IMediator mediator)
    {
        _deleteSystemMenu = deleteSystemMenu;
        _mediator = mediator;
    }

    public async Task<ISingleResultDto<EntityDto>> Create(SystemMenuCreateDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(SystemMenuEditDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(Guid id)
    {
        var result = await _deleteSystemMenu.Execute(id).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}