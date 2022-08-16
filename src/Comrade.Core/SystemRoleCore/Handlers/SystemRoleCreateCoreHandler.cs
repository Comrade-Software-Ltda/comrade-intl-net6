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
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly ISystemRoleRepository _repository;
    private readonly ISystemRoleCreateValidation _createValidation;

    public SystemRoleCreateCoreHandler(ISystemRoleCreateValidation createValidation, ISystemRoleRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _createValidation = createValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemRoleCreateCommand request, CancellationToken cancellationToken)
    {
        var validate = _createValidation.Execute(request);
        if (!validate.Success)
        {
            return validate;
        }
        await _repository.Add(request).ConfigureAwait(false);
        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        await _repository.Add(request).ConfigureAwait(false);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);
        return new CreateResult<Entity>(true, BusinessMessage.MSG01);
    }
}
