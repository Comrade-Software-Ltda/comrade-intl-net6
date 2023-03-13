using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleCreateValidation : ISystemRoleCreateValidation
{
    private readonly ISystemRoleNameUniqueValidation _systemRoleNameUniqueValidation;
    private readonly ISystemRoleTagUniqueValidation _systemRoleTagUniqueValidation;

    public SystemRoleCreateValidation(ISystemRoleNameUniqueValidation systemRoleNameUniqueValidation,
        ISystemRoleTagUniqueValidation systemRoleTagUniqueValidation)
    {
        _systemRoleNameUniqueValidation = systemRoleNameUniqueValidation;
        _systemRoleTagUniqueValidation = systemRoleTagUniqueValidation;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemRole entity)
    {
        var registerName = await _systemRoleNameUniqueValidation.Execute(entity).ConfigureAwait(false);
        var registerTag = await _systemRoleTagUniqueValidation.Execute(entity).ConfigureAwait(false);

        var messages = new List<string>();

        if (!registerName.Success && registerName.Message != null)
            messages.Add(registerName.Message);
        if (!registerTag.Success && registerTag.Message != null)
            messages.Add(registerTag.Message);

        if (registerName.Success && registerTag.Success)
            return new SingleResult<Entity>(entity);
        
        var result = new SingleResult<Entity>();
        result.Code = (int) EnumResponse.ErrorBusinessValidation;
        result.Success = false;
        result.Messages = messages;

        return result;
    }
}
