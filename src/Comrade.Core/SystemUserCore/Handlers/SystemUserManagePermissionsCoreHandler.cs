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
    SystemUserManagePermissionsCoreHandler : IRequestHandler<SystemUserManagePermissionsCommand, ISingleResult<Entity>>
{
    private readonly ISystemPermissionRepository _systemPermissionRepository;
    private readonly ISystemUserRepository _systemUserRepository;
    private readonly ISystemUserManagePermissionsValidation _validation;

    public SystemUserManagePermissionsCoreHandler(ISystemUserRepository systemUserRepository,
        ISystemPermissionRepository systemPermissionRepository, ISystemUserManagePermissionsValidation validation)
    {
        _systemUserRepository = systemUserRepository;
        _systemPermissionRepository = systemPermissionRepository;
        _validation = validation;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemUserManagePermissionsCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _systemUserRepository.GetByIdIncludePermissions(request.Id).ConfigureAwait(false);
        var permissions = _systemPermissionRepository.GetAll()
            .Where(permission => request.SystemPermissionIds.Contains(permission.Id)).ToList();

        if (user == null)
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);

        var validate = _validation.Execute(user);

        if (!validate.Success)
        {
            return validate;
        }

        user.SystemPermissions = permissions;

        await _systemUserRepository.BeginTransactionAsync().ConfigureAwait(false);
        _systemUserRepository.Update(user);
        await _systemUserRepository.CommitTransactionAsync().ConfigureAwait(false);

        return new CreateResult<Entity>(true,
            BusinessMessage.MSG01);
    }
}
