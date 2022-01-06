using System.Threading;
using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.AirplaneCore.Handlers;

public class
    AirplaneDeleteCoreHandler : IRequestHandler<AirplaneDeleteCommand, ISingleResult<Entity>>
{
    private readonly IAirplaneDeleteValidation _airplaneDeleteValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly IAirplaneRepository _repository;

    public AirplaneDeleteCoreHandler(IAirplaneDeleteValidation airplaneDeleteValidation,
        IAirplaneRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _airplaneDeleteValidation = airplaneDeleteValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(AirplaneDeleteCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = _airplaneDeleteValidation.Execute(recordExists);
        if (!validate.Success)
        {
            return validate;
        }

        var airplaneId = recordExists.Id;
        _repository.Remove(airplaneId);

        await _repository.BeginTransactionAsync().ConfigureAwait(false);
        _repository.Remove(airplaneId);
        await _repository.CommitTransactionAsync().ConfigureAwait(false);

        _mongoDbContext.DeleteOne<Airplane>(airplaneId);

        return new DeleteResult<Entity>(true,
            BusinessMessage.MSG03);
    }
}