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
    SystemUserManageRolesCoreHandler : IRequestHandler<SystemUserManageRolesCommand, ISingleResult<Entity>>
{
    private readonly ISystemRoleRepository _systemRoleRepository;
    private readonly ISystemUserRepository _systemUserRepository;
    private readonly ISystemUserManageRolesValidation _validation;

    public SystemUserManageRolesCoreHandler(ISystemUserManageRolesValidation validation,
        ISystemUserRepository systemUserRepository, ISystemRoleRepository systemRoleRepository)
    {
        _validation = validation;
        _systemUserRepository = systemUserRepository;
        _systemRoleRepository = systemRoleRepository;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemUserManageRolesCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _systemUserRepository.GetByIdIncludeRoles(request.Id).ConfigureAwait(false);
        var roles = _systemRoleRepository.GetAll()
            .Where(role => request.Roles.Contains(role.Id)).ToList();

        if (user == null)
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);

        var validate = _validation.Execute(user);

        if (!validate.Success)
        {
            return validate;
        }

        await _systemUserRepository.BeginTransactionAsync().ConfigureAwait(false);
        _systemUserRepository.Update(user);
        await _systemUserRepository.CommitTransactionAsync().ConfigureAwait(false);

        return new CreateResult<Entity>(true,
            BusinessMessage.MSG01);
    }
}
