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
    AirplaneEditCoreHandler : IRequestHandler<AirplaneEditCommand, ISingleResult<Entity>>
{
    private readonly IAirplaneEditValidation _airplaneEditValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly IAirplaneRepository _repository;

    public AirplaneEditCoreHandler(IAirplaneEditValidation airplaneEditValidation,
        IAirplaneRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _airplaneEditValidation = airplaneEditValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(AirplaneEditCommand request,
        CancellationToken cancellationToken)
    {
        var recordExists = await _repository.GetById(request.Id).ConfigureAwait(false);

        if (recordExists is null)
        {
            return new EditResult<Entity>(false,
                BusinessMessage.MSG04);
        }

        var validate = await _airplaneEditValidation.Execute(request, recordExists)
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

    private static void HydrateValues(Airplane target, Airplane source)
    {
        target.Code = source.Code;
        target.PassengerQuantity = source.PassengerQuantity;
        target.Model = source.Model;
    }
}
