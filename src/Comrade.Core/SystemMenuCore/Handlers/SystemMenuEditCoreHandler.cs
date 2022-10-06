using System.Threading;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;
using Comrade.Core.SystemMenuCore.Validations;

namespace Comrade.Core.SystemMenuCore.Handlers;

public class
    SystemMenuEditCoreHandler : IRequestHandler<SystemMenuEditCommand, ISingleResult<Entity>>
{
    private readonly ISystemMenuEditValidation _systemMenuEditValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemMenuRepository _repository;

    public SystemMenuEditCoreHandler(ISystemMenuEditValidation systemMenuEditValidation,
        ISystemMenuRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _systemMenuEditValidation = systemMenuEditValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemMenuEditCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);

        if (recordExists is null)
        {
            return new EditResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = await _systemMenuEditValidation.Execute(request, recordExists)
            .ConfigureAwait(false);

        if (!validate.Success)
        {
            return validate;
        }

        var obj = recordExists;
        HydrateValues(obj, request);

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Update(obj);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        _mongoDbContext.ReplaceOne(obj);

        return new EditResult<Entity>(true,
            BusinessMessage.MSG02);
    }

    private static void HydrateValues(SystemMenu target, SystemMenu source)
    {
        target.Description = source.Description;
        target.Menu = source.Menu;
        target.Route = source.Route;
        target.Text = source.Text;
    }
}