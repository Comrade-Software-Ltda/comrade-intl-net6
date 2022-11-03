using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Contracts
{
    public class SystemUserManageRolesDto : EntityDto, IRequest<SingleResultDto<EntityDto>>
    {
        public ICollection<Guid> SystemRoleIds { get; set; }
    }
}
