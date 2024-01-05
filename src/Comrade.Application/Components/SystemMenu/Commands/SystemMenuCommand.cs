using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemMenu.Contracts;
using Comrade.Core.SystemMenuCore;
using MediatR;

namespace Comrade.Application.Components.SystemMenu.Commands;

public class SystemMenuCommand(
    IUcSystemMenuDelete deleteSystemMenu,
    IMediator mediator)
    : ISystemMenuCommand
{
    public async Task<ISingleResultDto<EntityDto>> Create(SystemMenuCreateDto dto)
    {
        return await mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(SystemMenuEditDto dto)
    {
        return await mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(Guid id)
    {
        var result = await deleteSystemMenu.Execute(id);
        return new SingleResultDto<EntityDto>(result);
    }
}
