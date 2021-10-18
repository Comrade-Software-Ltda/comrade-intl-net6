using Comrade.Domain.Bases;

namespace Comrade.Application.Lookups;

public interface ILookupService<TEntity>
    where TEntity : Entity
{
    Task<IList<LookupDto>> GetLookup();
    Task<IList<LookupDto>> GetLookup(Expression<Func<TEntity, bool>> predicate);
}