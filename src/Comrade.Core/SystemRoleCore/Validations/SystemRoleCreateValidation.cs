using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleCreateValidation(
    ISystemRoleNameUniqueValidation systemRoleNameUniqueValidation,
    ISystemRoleTagUniqueValidation systemRoleTagUniqueValidation)
    : ISystemRoleCreateValidation
{
    public async Task<ISingleResult<Entity>> Execute(SystemRole entity)
    {
        var registerName = await systemRoleNameUniqueValidation.Execute(entity);
        var registerTag = await systemRoleTagUniqueValidation.Execute(entity);

        var messages = new List<string>();

        if (!registerName.Success && registerName.Message != null)
            messages.Add(registerName.Message);
        if (!registerTag.Success && registerTag.Message != null)
            messages.Add(registerTag.Message);

        if (registerName.Success && registerTag.Success)
            return new SingleResult<Entity>(entity);

        var result = new SingleResult<Entity>((int) EnumResponse.ErrorBusinessValidation, messages);

        return result;
    }
}
