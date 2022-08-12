using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Core.SystemMenuCore.Validations;
using Comrade.Domain.Bases;
using MediatR;
using System.Threading;

namespace Comrade.Core.SystemMenuCore.Handlers;

public class
    SystemMenuDeleteCoreHandler : IRequestHandler<SystemMenuDeleteCommand, ISingleResult<Entity>>
{
    private readonly SystemMenuDeleteValidation _systemMenuDeleteValidation;
    private readonly ISystemMenuRepository _repository;

    public SystemMenuDeleteCoreHandler(SystemMenuDeleteValidation systemMenuDeleteValidation,
        ISystemMenuRepository repository)
    {
        _systemMenuDeleteValidation = systemMenuDeleteValidation;
        _repository = repository;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemMenuDeleteCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = _systemMenuDeleteValidation.Execute(recordExists);
        if (!validate.Success)
        {
            return validate;
        }

        var systemMenuId = recordExists.Id;

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Remove(systemMenuId);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        return new DeleteResult<Entity>(true,
            BusinessMessage.MSG03);
    }
}