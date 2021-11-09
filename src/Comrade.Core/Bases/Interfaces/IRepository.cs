using Comrade.Domain.Bases;
using Comrade.Domain.Bases.Interfaces;

namespace Comrade.Core.Bases.Interfaces;

public interface IRepository<TEntity> : IDisposable
    where TEntity : IEntity
{
    Task CommitChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task Add(TEntity obj);
    Task AddAll(IList<TEntity> obj);
    void Update(TEntity obj);
    void UpdateAll(IList<TEntity> obj);
    void Remove(Guid id);
    void RemoveAll(IList<Guid> id);
    Task<TEntity?> GetById(Guid id);
    Task<TEntity?> GetById(Guid id, params string[] includes);
    Task<TEntity?> GetById(Guid id, Expression<Func<TEntity, TEntity>> projection);

    Task<TEntity?> GetById(Guid id, Expression<Func<TEntity, TEntity>>? projection,
        params string[]? includes);

    Task<TEntity?> GetByValue(string value);
    Task<TEntity?> GetByValue(string value, Expression<Func<TEntity, TEntity>>? projection);
    Task<bool> ValueExists(Guid id, string value);
    Task<bool> IsUnique(Expression<Func<TEntity, bool>> predicate);
    IQueryable<Lookup> GetLookup();
    IQueryable<Lookup> GetLookup(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> GetLookupQuery(Expression<Func<TEntity, TEntity>> projection);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAllAsNoTracking();
    IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, TEntity>> projection);
    Task<TEntity?> GetByPredicate(Expression<Func<TEntity, bool>> predicate);
}