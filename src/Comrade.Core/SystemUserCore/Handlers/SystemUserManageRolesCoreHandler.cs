using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemRoleCore;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.Handlers;

public class
    SystemUserManageRolesCoreHandler(
        ISystemUserManageRolesValidation validation,
        ISystemUserRepository systemUserRepository,
        ISystemRoleRepository systemRoleRepository)
    : IRequestHandler<SystemUserManageRolesCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemUserManageRolesCommand request,
        CancellationToken cancellationToken)
    {
        var user = await systemUserRepository.GetByIdIncludeRoles(request.Id);
        var roles = systemRoleRepository.GetAll()
            .Where(role => request.SystemRoleIds.Contains(role.Id)).ToList();

        if (user == null)
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);

        var validate = validation.Execute(user);

        if (!validate.Success)
        {
            return validate;
        }

        await systemUserRepository.BeginTransactionAsync();
        systemUserRepository.Update(user);
        await systemUserRepository.CommitTransactionAsync();

        return new CreateResult<Entity>(true,
            BusinessMessage.MSG01);
    }
}
