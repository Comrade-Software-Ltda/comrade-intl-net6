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

public class SystemRoleManagePermissionsCoreHandler
    : IRequestHandler<SystemRoleManagePermissionsCommand, ISingleResult<Entity>>
{
    private readonly ISystemRoleRepository _repository;
    private readonly ISystemPermissionRepository _systemPermissionRepository;
    private readonly ISystemRoleManagePermissionsValidation _validation;

    public SystemRoleManagePermissionsCoreHandler(ISystemRoleRepository repository,
        ISystemRoleManagePermissionsValidation validation, ISystemPermissionRepository systemPermissionRepository)
    {
        _repository = repository;
        _validation = validation;
        _systemPermissionRepository = systemPermissionRepository;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemRoleManagePermissionsCommand request,
        CancellationToken cancellationToken)
    {
        var role = await _repository.GetByIdIncludePermissions(request.Id).ConfigureAwait(false);
        var permissions = _systemPermissionRepository.GetAll()
            .Where(permission => request.SystemPermissionIds.Contains(permission.Id)).ToList();

        if (role == null)
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);

        var validate = _validation.Execute(role);

        if (!validate.Success)
        {
            return validate;
        }

        role.SystemPermissions = permissions;

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Update(role);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        return new CreateResult<Entity>(true, BusinessMessage.MSG01);
    }
}
