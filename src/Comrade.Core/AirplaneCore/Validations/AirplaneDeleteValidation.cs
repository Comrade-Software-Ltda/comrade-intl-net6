using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneDeleteValidation : IAirplaneDeleteValidation
{
    public ISingleResult<Entity> Execute(Airplane? recordExists)
    {
        return new SingleResult<Entity>(recordExists);
    }
}