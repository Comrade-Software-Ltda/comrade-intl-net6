﻿using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;
using MediatR;
using System.Threading;

namespace Comrade.Core.SystemUserCore.Handlers;

public class
    SystemUserDeleteCoreHandler : IRequestHandler<SystemUserDeleteCommand, ISingleResult<Entity>>
{
    private readonly IMongoDbContext _mongoDbContext;
    private readonly ISystemUserRepository _repository;
    private readonly SystemUserDeleteValidation _systemUserDeleteValidation;

    public SystemUserDeleteCoreHandler(SystemUserDeleteValidation systemUserDeleteValidation,
        ISystemUserRepository repository, IMongoDbContext mongoDbContext)
    {
        _systemUserDeleteValidation = systemUserDeleteValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(SystemUserDeleteCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = _systemUserDeleteValidation.Execute(recordExists);
        if (!validate.Success)
        {
            return validate;
        }

        _repository.Remove(request.Id);

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Remove(recordExists.Id);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        return new DeleteResult<Entity>(true,
            BusinessMessage.MSG03);
    }
}