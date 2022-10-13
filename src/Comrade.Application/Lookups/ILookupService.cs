using Comrade.Domain.Bases;

namespace Comrade.Application.Lookups;

public interface ILookupService<TEntity>
    where TEntity : Entity
{
    Task<List<LookupDto>> GetLookup();
    Task<List<LookupDto>> GetLookup(Expression<Func<TEntity, bool>> predicate);
}
