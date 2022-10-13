using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Core.SystemRoleCore.Validations;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemRoleCore.Handlers;

public class SystemRoleCreateCoreHandler : IRequestHandler<SystemRoleCreateCommand, ISingleResult<Entity>>
{
    private readonly ISystemRoleCreateValidation _createValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemRoleRepository _repository;

    public SystemRoleCreateCoreHandler(ISystemRoleCreateValidation createValidation, ISystemRoleRepository repository,
        IMongoDbCommandContext mongoDbContext)
    {
        _createValidation = createValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemRoleCreateCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _createValidation.Execute(request).ConfigureAwait(false);
        if (!result.Success)
        {
            return result;
        }

        HydrateValues(request);
        await _repository.Add(request).ConfigureAwait(false);
        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        await _repository.Add(request).ConfigureAwait(false);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);
        return new CreateResult<Entity>(true, BusinessMessage.MSG01);
    }

    private static void HydrateValues(SystemRoleCreateCommand target)
    {
        target.Name = target.Name.ToUpper(CultureInfo.CurrentCulture).Trim();
    }
}
