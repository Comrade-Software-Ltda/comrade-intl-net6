#region

using Comrade.Domain.Models;
using Comrade.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Comrade.Persistence.DataAccess;

public class ComradeContext : DbContext
{
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
}
