using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Bases.Interfaces;

namespace Comrade.Core.Bases;

public class UseCase : IUseCase
{
    public static ISingleResult<Entity> ValidateEntity<T>(T entity) where T : IEntity
    {
        var context = new ValidationContext(entity, null, null);
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        var valid = Validator.TryValidateObject(entity, context, validationResults, true);
        if (!valid)
        {
            var listErrors = validationResults.Select(x => x.ErrorMessage);
            return new SingleResult<Entity>(listErrors!);
        }

        return new SingleResult<Entity>();
    }
}
