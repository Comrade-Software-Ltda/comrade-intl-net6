﻿using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleEditValidation : ISystemRoleEditValidation
{
    private readonly ISystemRoleNameUniqueValidation _systemRoleNameUniqueValidation;

    public SystemRoleEditValidation(ISystemRoleNameUniqueValidation systemRoleNameUniqueValidation)
    {
        _systemRoleNameUniqueValidation = systemRoleNameUniqueValidation;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemRole entity, SystemRole? recordExists)
    {
        var register = await _systemRoleNameUniqueValidation.Execute(entity);
        return register.Success ? new SingleResult<Entity>(recordExists) : register;
    }
}
