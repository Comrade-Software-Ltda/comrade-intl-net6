using System.Data;
using Comrade.Domain.Models;
using Comrade.Persistence.Mappings;
using Microsoft.EntityFrameworkCore.Storage;

namespace Comrade.Persistence.DataAccess;

public class ComradeContext : DbContext
{
    private IDbContextTransaction _currentTransaction;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public ComradeContext(DbContextOptions<ComradeContext> options)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        : base(options)
    {
    }

    // Tables
    public DbSet<Airplane> Airplanes { get; set; }
    public DbSet<SystemUser> SystemUsers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tables
        modelBuilder.ApplyConfiguration(new AirplaneConfiguration());
        modelBuilder.ApplyConfiguration(new SystemUserConfiguration());
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            return;
        }

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted)
            .ConfigureAwait(false);
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await SaveChangesAsync().ConfigureAwait(false);

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
}