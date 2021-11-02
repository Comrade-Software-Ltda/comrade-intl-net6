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
    AirplaneCreateCoreHandler : IRequestHandler<AirplaneCreateCommand, ISingleResult<Entity>>
{
    private readonly AirplaneCreateValidation _airplaneCreateValidation;
    private readonly IAirplaneRepository _repository;
    private readonly IMongoDbClient _mongoDbClient;

    public AirplaneCreateCoreHandler(AirplaneCreateValidation airplaneCreateValidation,
        IAirplaneRepository repository, IMongoDbClient mongoDbClient)
    {
        _airplaneCreateValidation = airplaneCreateValidation;
        _repository = repository;
        _mongoDbClient = mongoDbClient;
    }

    public async Task<ISingleResult<Entity>> Handle(AirplaneCreateCommand request,
        CancellationToken cancellationToken)
    {
        var validate = await _airplaneCreateValidation.Execute(request).ConfigureAwait(false);
        if (!validate.Success)
        {
            return validate;
        }

        request.RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia();
        await _repository.AddCommit(request).ConfigureAwait(false);

        _mongoDbClient.InsertOne(request);

        return new CreateResult<Entity>(true,
            BusinessMessage.MSG01);
    }
}