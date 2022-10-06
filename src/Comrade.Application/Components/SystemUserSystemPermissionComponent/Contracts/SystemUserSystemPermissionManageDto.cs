using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUserSystemPermissionComponent.Contracts
{
    public class SystemUserSystemPermissionManageDto : EntityDto, IRequest<SingleResultDto<EntityDto>>
    {
        public SystemUserSystemPermissionManageDto(ICollection<Guid> permissions)
        {
            Permissions = permissions;
        }

        public ICollection<Guid> Permissions { get; set; }
    }
}
