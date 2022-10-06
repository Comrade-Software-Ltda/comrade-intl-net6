using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using MediatR;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Commands
{
    public class SystemUserSystemRoleCommand: ISystemUserSystemRoleCommand
    {
        private readonly IMediator _mediator;
        public SystemUserSystemRoleCommand(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ISingleResultDto<EntityDto>> Manage(SystemUserSystemRoleManageDto dto)
        {
            return await _mediator.Send(dto).ConfigureAwait(false);
        }
    }
}
