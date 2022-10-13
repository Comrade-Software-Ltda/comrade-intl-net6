using Comrade.Domain.Models;
using Comrade.Persistence.Mappings;

namespace Comrade.Persistence.DataAccess;

public class ComradeContext : DbContext
{
    public ComradeContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Airplane> Airplanes { get; set; }
    public DbSet<SystemPermission> SystemPermissions { get; set; }
    public DbSet<SystemRole> SystemRoles { get; set; }
    public DbSet<SystemRoleSystemPermission> SystemRolePermissions { get; set; }
    public DbSet<SystemUser> SystemUsers { get; set; }
    public DbSet<SystemUserSystemRole> SystemUserRoles { get; set; }
    public DbSet<SystemUserSystemPermission> SystemUserPermissions { get; set; }
    public DbSet<SystemMenu> SystemMenus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AirplaneConfiguration());
        modelBuilder.ApplyConfiguration(new SystemMenuConfiguration());
        modelBuilder.ApplyConfiguration(new SystemPermissionConfiguration());
        modelBuilder.ApplyConfiguration(new SystemRoleConfiguration());
        modelBuilder.ApplyConfiguration(new SystemRolePermissionConfiguration());
        modelBuilder.ApplyConfiguration(new SystemUserConfiguration());
        modelBuilder.ApplyConfiguration(new SystemUserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new SystemUserPermissionConfiguration());
    }
}
