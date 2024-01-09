using Comrade.Core.Bases.Interfaces;

namespace Comrade.Persistence.DataAccess;

public sealed class UnitOfWork(ComradeContext context) : IUnitOfWork, IDisposable
{
    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<bool> Commit()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<int> AffectedRows()
    {
        var affectedRows = await context
                .SaveChangesAsync()
            ;
        return affectedRows;
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            context.Dispose();
        }

        _disposed = true;
    }
}
