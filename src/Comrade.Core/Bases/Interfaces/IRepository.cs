using Comrade.Domain.Bases;
using Comrade.Domain.Bases.Interfaces;

namespace Comrade.Core.Bases.Interfaces;

public interface IRepository<TEntity> : IDisposable
    where TEntity : IEntity
{
    Task Add(TEntity obj);
    Task AddCommit(TEntity obj);
    Task AddAll(IList<TEntity> obj);
    Task AddAllCommit(IList<TEntity> obj);
    void Update(TEntity obj);
    void UpdateCommit(TEntity obj);
    void UpdateAll(IList<TEntity> obj);
    void UpdateAllCommit(IList<TEntity> obj);
    void Remove(int id);
    void RemoveCommit(int id);
    void RemoveAll(IList<int> id);
    void RemoveAllCommit(IList<int> id);
    Task<TEntity?> GetById(int id);
    Task<TEntity?> GetById(int id, params string[] includes);
    Task<TEntity?> GetById(int id, Expression<Func<TEntity, TEntity>> projection);

    Task<TEntity?> GetById(int id, Expression<Func<TEntity, TEntity>>? projection,
        params string[]? includes);

    Task<TEntity?> GetByValue(string value);
    Task<TEntity?> GetByValue(string value, Expression<Func<TEntity, TEntity>>? projection);
    Task<bool> ValueExists(int id, string value);
    Task<bool> IsUnique(Expression<Func<TEntity, bool>> predicate);
    IQueryable<Lookup> GetLookup();
    IQueryable<Lookup> GetLookup(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> GetLookupQuery(Expression<Func<TEntity, TEntity>> projection);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAllAsNoTracking();
    IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, TEntity>> projection);
    Task<TEntity?> GetByPredicate(Expression<Func<TEntity, bool>> predicate);
}