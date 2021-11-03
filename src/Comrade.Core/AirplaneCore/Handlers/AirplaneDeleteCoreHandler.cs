using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Extensions;
using MediatR;
using System.Threading;

namespace Comrade.Core.AirplaneCore.Handlers;

public class
    AirplaneDeleteCoreHandler : IRequestHandler<AirplaneDeleteCommand, ISingleResult<Entity>>
{
    private readonly AirplaneDeleteValidation _airplaneDeleteValidation;
    private readonly IAirplaneRepository _repository;
    private readonly IMongoDbContext _mongoDbContext;

    public AirplaneDeleteCoreHandler(AirplaneDeleteValidation airplaneDeleteValidation,
        IAirplaneRepository repository, IMongoDbContext mongoDbContext)
    {
        _airplaneDeleteValidation = airplaneDeleteValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(AirplaneDeleteCommand request,
        CancellationToken cancellationToken)
    {
        var validate = _airplaneDeleteValidation.Execute(request);
        if (!validate.Success)
        {
            return validate;
        }

        request.RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia();
        _repository.Remove(request.Id);
        await _repository.CommitChangesAsync().ConfigureAwait(false);

        _mongoDbContext.InsertOne(request);

        return new DeleteResult<Entity>(true,
            BusinessMessage.MSG01);
    }
}