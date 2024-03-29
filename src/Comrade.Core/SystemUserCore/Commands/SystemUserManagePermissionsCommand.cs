﻿using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.Commands;

public class SystemUserManagePermissionsCommand : Entity, IRequest<ISingleResult<Entity>>
{
    public ICollection<Guid> SystemPermissionIds { get; set; }
}
