using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemRoleCore;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Core.SystemUserSystemRoleCore.Validations;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserSystemRoleCore.Handlers;

public class
    SystemUserSystemRoleManageCoreHandler : IRequestHandler<SystemUserSystemRoleManageCommand, ISingleResult<Entity>>
{
    private readonly ISystemUserSystemRoleManageValidation _validation;
    private readonly ISystemUserRepository _systemUserRepository;
    private readonly ISystemRoleRepository _systemRoleRepository;
    public SystemUserSystemRoleManageCoreHandler(ISystemUserSystemRoleManageValidation validation,
        ISystemUserRepository systemUserRepository, ISystemRoleRepository systemRoleRepository)
    {
        _validation = validation;
        _systemUserRepository = systemUserRepository;
        _systemRoleRepository = systemRoleRepository;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemUserSystemRoleManageCommand request,
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