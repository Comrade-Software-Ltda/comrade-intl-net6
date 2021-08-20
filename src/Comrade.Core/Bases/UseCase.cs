#region

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases.Interfaces;

#endregion

namespace Comrade.Core.Bases
{
    public class UseCase : IUseCase
    {
        private readonly IUnitOfWork _uow;

        public UseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Commit()
        {
            if (await _uow.Commit().ConfigureAwait(false)) return true;

            return false;
        }

        public static ISingleResult<T> ValidateEntity<T>(T entity) where T : IEntity
        {
            var context = new ValidationContext(entity, null, null);
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(entity, context, validationResults, true);
            if (!valid)
            {
                var listErrors = validationResults.Select(x => x.ErrorMessage);
                return new SingleResult<T>(listErrors!);
            }

            return new SingleResult<T>();
        }
    }
}