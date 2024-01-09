using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemPermissionCore;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Core.SystemRoleCore.Validations;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemRoleCore.Handlers;

public class SystemRoleManagePermissionsCoreHandler(
    ISystemRoleRepository repository,
    ISystemRoleManagePermissionsValidation validation,
    ISystemPermissionRepository systemPermissionRepository)
    : IRequestHandler<SystemRoleManagePermissionsCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(SystemRoleManagePermissionsCommand request,
        CancellationToken cancellationToken)
    {
        var role = await repository.GetByIdIncludePermissions(request.Id);
        var permissions = systemPermissionRepository.GetAll()
            .Where(permission => request.SystemPermissionIds.Contains(permission.Id)).ToList();

        if (role == null)
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);

        var validate = validation.Execute(role);

        if (!validate.Success)
        {
            return validate;
        }

        role.SystemPermissions = permissions;

        await repository.BeginTransactionAsync();
        repository.Update(role);
        await repository.CommitTransactionAsync();

        return new CreateResult<Entity>(true, BusinessMessage.MSG01);
    }
}
