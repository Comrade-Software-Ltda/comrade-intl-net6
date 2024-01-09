using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemPermissionCore;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.Handlers;

public class
    SystemUserManagePermissionsCoreHandler(
        ISystemUserRepository systemUserRepository,
        ISystemPermissionRepository systemPermissionRepository,
        ISystemUserManagePermissionsValidation validation)
    : IRequestHandler<SystemUserManagePermissionsCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemUserManagePermissionsCommand request,
        CancellationToken cancellationToken)
    {
        var user = await systemUserRepository.GetByIdIncludePermissions(request.Id);
        var permissions = systemPermissionRepository.GetAll()
            .Where(permission => request.SystemPermissionIds.Contains(permission.Id)).ToList();

        if (user == null)
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);

        var validate = validation.Execute(user);

        if (!validate.Success)
        {
            return validate;
        }

        user.SystemPermissions = permissions;

        await systemUserRepository.BeginTransactionAsync();
        systemUserRepository.Update(user);
        await systemUserRepository.CommitTransactionAsync();

        return new CreateResult<Entity>(true, BusinessMessage.MSG01);
    }
}
