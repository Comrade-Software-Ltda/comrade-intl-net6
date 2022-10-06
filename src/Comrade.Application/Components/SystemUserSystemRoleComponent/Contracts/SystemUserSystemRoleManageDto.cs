using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts
{
    public class SystemUserSystemRoleManageDto : EntityDto, IRequest<SingleResultDto<EntityDto>>
    {
        public SystemUserSystemRoleManageDto(ICollection<Guid> roles)
        {
            Roles = roles;
        }

        public ICollection<Guid> Roles { get; set; }
    }
}
