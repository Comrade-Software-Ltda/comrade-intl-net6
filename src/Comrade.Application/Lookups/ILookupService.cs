#region

using Comrade.Domain.Bases;
using System.Linq.Expressions;

#endregion

namespace Comrade.Application.Lookups;

public interface ILookupService<TEntity>
        where TEntity : Entity
{
    Task<IList<LookupDto>> GetLookup();
    Task<IList<LookupDto>> GetLookup(Expression<Func<TEntity, bool>> predicate);
}
