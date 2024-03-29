﻿using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUser.Contracts;

public class SystemUserManagePermissionsDto : EntityDto, IRequest<SingleResultDto<EntityDto>>
{
    public ICollection<Guid> SystemPermissionIds { get; set; }
}
