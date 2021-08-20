#region

using System.Threading.Tasks;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Messages;
using Comrade.Domain.Bases;

#endregion

namespace Comrade.Core.Bases.Validations
{
    public class EntityValidation<TEntity> : IEntityValidation<TEntity>
        where TEntity : Entity
    {
        private readonly IRepository<TEntity> _repository;

        public EntityValidation(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<ISingleResult<TEntity>> RecordExists(int id, params string[] includes)
        {
            var entity = await _repository.GetById(id, includes).ConfigureAwait(false);
            if (entity == null) return new SingleResult<TEntity>(BusinessMessage.MSG04);

            return new SingleResult<TEntity>(entity);
        }
    }
}