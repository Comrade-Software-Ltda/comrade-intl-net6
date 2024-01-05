using System.Threading;
using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Extensions;
using MediatR;

namespace Comrade.Core.AirplaneCore.Handlers;

public class
    AirplaneCreateCoreHandler : IRequestHandler<AirplaneCreateCommand, ISingleResult<Entity>>
{
    private readonly IAirplaneCreateValidation _airplaneCreateValidation;
    private readonly IMongoDbCommandContext _mongoDbContext;
    private readonly IAirplaneRepository _repository;

    public AirplaneCreateCoreHandler(IAirplaneCreateValidation airplaneCreateValidation,
        IAirplaneRepository repository, IMongoDbCommandContext mongoDbContext)
    {
        _airplaneCreateValidation = airplaneCreateValidation;
        _repository = repository;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ISingleResult<Entity>> Handle(AirplaneCreateCommand request,
        CancellationToken cancellationToken)
    {
        var validate = await _airplaneCreateValidation.Execute(request);
        if (!validate.Success)
        {
            return validate;
        }

        request.RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia();

        await _repository.BeginTransactionAsync();
        await _repository.Add(request);
        await _repository.CommitTransactionAsync();

        _mongoDbContext.InsertOne(request);

        return new CreateResult<Entity>(true,
            BusinessMessage.MSG01);
    }
}
