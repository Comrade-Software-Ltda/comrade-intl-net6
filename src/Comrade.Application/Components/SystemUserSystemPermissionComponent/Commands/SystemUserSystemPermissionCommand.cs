using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserSystemPermissionComponent.Contracts;
using MediatR;

namespace Comrade.Application.Components.SystemUserSystemPermissionComponent.Commands
{
    public class SystemUserSystemPermissionCommand : ISystemUserSystemPermissionCommand
    {
        private readonly IMediator _mediator;
        public SystemUserSystemPermissionCommand(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ISingleResultDto<EntityDto>> Manage(SystemUserSystemPermissionManageDto dto)
        {
            return await _mediator.Send(dto).ConfigureAwait(false);
        }
    }
}
