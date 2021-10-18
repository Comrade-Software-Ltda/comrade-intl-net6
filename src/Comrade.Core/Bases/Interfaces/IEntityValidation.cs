using Comrade.Domain.Bases.Interfaces;

namespace Comrade.Core.Bases.Interfaces;

public interface IEntityValidation<TEntity>
    where TEntity : IEntity
{
    Task<ISingleResult<TEntity>> RecordExists(int id, params string[] includes);
}