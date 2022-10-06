using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemPermissionCore;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserSystemPermissionCore.Commands;
using Comrade.Core.SystemUserSystemPermissionCore.Validations;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserSystemPermissionCore.Handlers;

public class
    SystemUserSystemPermissionManageCoreHandler : IRequestHandler<SystemUserSystemPermissionManageCommand, ISingleResult<Entity>>
{
    private readonly ISystemUserSystemPermissionManageValidation _validation;
    private readonly ISystemUserRepository _systemUserRepository;
    private readonly ISystemPermissionRepository _systemPermissionRepository;
    public SystemUserSystemPermissionManageCoreHandler(ISystemUserRepository systemUserRepository,
        ISystemPermissionRepository systemPermissionRepository, ISystemUserSystemPermissionManageValidation validation)
    {
        _systemUserRepository = systemUserRepository;
        _systemPermissionRepository = systemPermissionRepository;
        _validation = validation;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemUserSystemPermissionManageCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _systemUserRepository.GetByIdIncludePermissions(request.Id).ConfigureAwait(false);
        var permissions = _systemPermissionRepository.GetAll()
            .Where(permission => request.Permissions.Contains(permission.Id)).ToList();

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