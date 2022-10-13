using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.Extensions;

namespace Comrade.Persistence.Mappings;

public class SystemRolePermissionConfiguration : BaseMappingConfiguration<SystemRole>
{
    public override void Configure(EntityTypeBuilder<SystemRole> builder)
    {
        builder
            .HasMany(p => p.SystemPermissions)
            .WithMany(p => p.SystemRoles)
            .UsingEntity<SystemRoleSystemPermission>(BuildSystemRoleSystemPermissionRelationship);
    }

    private static void BuildSystemRoleSystemPermissionRelationship(
        EntityTypeBuilder<SystemRoleSystemPermission> builder)
    {
        builder
            .HasOne(pt => pt.SystemPermission)
            .WithMany(t => t.SystemRolePermissions)
            .HasForeignKey(pt => pt.SystemPermissionId);
        builder
            .HasOne(pt => pt.SystemRole)
            .WithMany(p => p.SystemRolePermissions)
            .HasForeignKey(pt => pt.SystemRoleId);

        builder.Property(b => b.Id).HasColumnName("pk_uuid_syro_system_role_sype_system_permission").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_syro_system_role_sype_system_permission");

        builder.HasKey(t => new {t.SystemRoleId, t.SystemPermissionId})
            .HasName("rl_syro_system_role_sype_system_permission");

        builder.InsertSeedData($"{SeedJsonBasePath}.system-role-permission.json");
    }
}
