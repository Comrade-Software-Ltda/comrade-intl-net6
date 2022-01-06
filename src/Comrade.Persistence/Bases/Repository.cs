using System.Data;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore.Storage;

namespace Comrade.Persistence.Bases;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    private readonly ComradeContext _db;
    private readonly DbSet<TEntity> _dbSet;
    private IDbContextTransaction? _currentTransaction;
    private bool _disposed;

    public Repository(ComradeContext context)
    {
        _db = context;
        _dbSet = _db.Set<TEntity>();
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            return;
        }

        _currentTransaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted)
            .ConfigureAwait(false);
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _db.SaveChangesAsync().ConfigureAwait(false);

            await (_currentTransaction?.CommitAsync() ?? Task.CompletedTask).ConfigureAwait(false);
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public virtual async Task CommitChangesAsync()
    {
        await _db.SaveChangesAsync().ConfigureAwait(false);
    }

    public virtual async Task Add(TEntity obj)
    {
        await _dbSet.AddAsync(obj).ConfigureAwait(false);
    }

    public virtual async Task AddAll(IList<TEntity> obj)
    {
        await _dbSet.AddRangeAsync(obj).ConfigureAwait(false);
    }

    public virtual void Update(TEntity obj)
    {
        _dbSet.Update(obj);
    }

    public virtual void UpdateAll(IList<TEntity> obj)
    {
        _dbSet.UpdateRange(obj);
    }

    public virtual void Remove(Guid id)
    {
        var removedItem = _dbSet.Find(id);
        if (removedItem != null)
        {
            _dbSet.Remove(removedItem);
        }
    }

    public virtual void RemoveAll(IList<Guid> id)
    {
        var remove = _dbSet.Where(x => id.Contains(x.Id));
        _dbSet.RemoveRange(remove);
    }

    public virtual async Task<TEntity?> GetById(Guid id)
    {
        return await GetById(id, null, includes: null).ConfigureAwait(false);
    }

    public virtual async Task<TEntity?> GetById(Guid id, params string[] includes)
    {
        return await GetById(id, null, includes).ConfigureAwait(false);
    }

    public virtual async Task<TEntity?> GetById(Guid id,
        Expression<Func<TEntity, TEntity>> projection)
    {
        return await GetById(id, projection, null).ConfigureAwait(false);
    }

    public virtual async Task<TEntity?> GetById(Guid id,
        Expression<Func<TEntity, TEntity>>? projection,
        params string[]? includes)
    {
        var query = GetAll();
        if (projection != null) query = query.Select(projection);

        if (includes is {Length: > 0})
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        query = query.Where(p => p.Id == id);

        return await query.FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public virtual async Task<TEntity?> GetByValue(string value)
    {
        return await GetByValue(value, null).ConfigureAwait(false);
    }

    public virtual async Task<TEntity?> GetByValue(string value,
        Expression<Func<TEntity, TEntity>>? projection)
    {
        var query = GetAll();
        if (projection != null) query = query.Select(projection);

        query = query.Where(p => p.Value == value);

        return await query.FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public virtual async Task<bool> ValueExists(Guid id, string value)
    {
        var exists = await GetAll()
            .Where(p => p.Id != id
                        && p.Value == value)
            .AnyAsync().ConfigureAwait(false);

        return exists;
    }

    public virtual async Task<bool> IsUnique(Expression<Func<TEntity, bool>> predicate)
    {
        var query = GetAll();

        var exists = await query
            .Where(predicate)
            .AnyAsync().ConfigureAwait(false);

        return !exists;
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return _dbSet;
    }

    public virtual IQueryable<TEntity> GetAllAsNoTracking()
    {
        return _dbSet.AsNoTracking();
    }

    public IEnumerable<TEntity> GetAllAsNoTracking(
        Expression<Func<TEntity, TEntity>> projection)
    {
        return _dbSet.AsNoTracking().Select(projection);
    }

    public async Task<TEntity?> GetByPredicate(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.SingleOrDefaultAsync(predicate).ConfigureAwait(false);
    }

    public virtual IQueryable<Lookup> GetLookup()
    {
        return _dbSet
            .Take(100)
            .Select(s => new Lookup {Key = s.Key, Value = s.Value});
    }

    public IQueryable<Lookup> GetLookup(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet
            .AsNoTracking()
            .Take(100)
            .Where(predicate)
            .Select(s => new Lookup {Key = s.Key, Value = s.Value});
    }


    public virtual IQueryable<TEntity> GetLookupQuery(
        Expression<Func<TEntity, TEntity>> projection)
    {
        var query = GetAll();

        query = query.Select(projection);

        return query;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _db.Dispose();
        }

        _disposed = true;
    }
}