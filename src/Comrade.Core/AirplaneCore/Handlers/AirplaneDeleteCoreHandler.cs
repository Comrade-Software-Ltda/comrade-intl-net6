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
    AirplaneDeleteCoreHandler(
        IAirplaneDeleteValidation airplaneDeleteValidation,
        IAirplaneRepository repository,
        IMongoDbCommandContext mongoDbContext)
    : IRequestHandler<AirplaneDeleteCommand, ISingleResult<Entity>>
{
    public async Task<ISingleResult<Entity>> Handle(AirplaneDeleteCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await repository.GetById(request.Id);

        if (recordExists is null)
        {
            return new DeleteResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = airplaneDeleteValidation.Execute(recordExists);
        if (!validate.Success)
        {
            return validate;
        }

        var airplaneId = recordExists.Id;
        repository.Remove(airplaneId);

        await repository.BeginTransactionAsync();
        repository.Remove(airplaneId);
        await repository.CommitTransactionAsync();

        mongoDbContext.DeleteOne<Airplane>(airplaneId);

        return new DeleteResult<Entity>(true,
            BusinessMessage.MSG03);
    }
}
