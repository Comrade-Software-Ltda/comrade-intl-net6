using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Core.SystemRoleCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemRoleCore.Handlers;

public class SystemRoleEditCoreHandler : IRequestHandler<SystemRoleEditCommand, ISingleResult<Entity>>
{
    private readonly ISystemRoleEditValidation _editValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemRoleRepository _repository;

    public SystemRoleEditCoreHandler(ISystemRoleEditValidation editValidation, ISystemRoleRepository repository,
        IMongoDbCommandContext mongoDbContext)
    {
        _editValidation = editValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemRoleEditCommand request, CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);
        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false, BusinessMessage.MSG04);
        }

        var result = await _editValidation.Execute(request, recordExists).ConfigureAwait(false);
        if (!result.Success)
        {
            return result;
        }

        var obj = recordExists;
        HydrateValues(obj, request);
        _repository.Update(obj);

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Update(obj);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        _mongoDbContext.ReplaceOne(obj);

        return new EditResult<Entity>(true, BusinessMessage.MSG02);
    }

    private static void HydrateValues(SystemRole target, SystemRole source)
    {
        target.Name = source.Name.ToUpper(CultureInfo.CurrentCulture).Trim();
    }
}
