using Comrade.Core.Bases.Interfaces;

namespace Comrade.Persistence.DataAccess;

public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ComradeContext _context;
    private bool _disposed;

    public UnitOfWork(ComradeContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<bool> Commit()
    {
        return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
    }

    public async Task<int> AffectedRows()
    {
        var affectedRows = await _context
            .SaveChangesAsync()
            .ConfigureAwait(false);
        return affectedRows;
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }

        _disposed = true;
    }
}
