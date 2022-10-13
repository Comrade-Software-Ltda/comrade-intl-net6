using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.Extensions;

namespace Comrade.Persistence.Mappings;

public class SystemUserPermissionConfiguration : BaseMappingConfiguration<SystemUser>
{
    public override void Configure(EntityTypeBuilder<SystemUser> builder)
    {
        builder
            .HasMany(p => p.SystemPermissions)
            .WithMany(p => p.SystemUsers)
            .UsingEntity<SystemUserSystemPermission>(BuildSystemUserSystemPermissionRelationship);
    }

    private static void BuildSystemUserSystemPermissionRelationship(
        EntityTypeBuilder<SystemUserSystemPermission> builder)
    {
        builder
            .HasOne(pt => pt.SystemPermission)
            .WithMany(t => t.SystemUserPermissions)
            .HasForeignKey(pt => pt.SystemPermissionId);
        builder
            .HasOne(pt => pt.SystemUser)
            .WithMany(p => p.SystemUserPermissions)
            .HasForeignKey(pt => pt.SystemUserId);
        builder.Property(b => b.Id).HasColumnName("pk_uuid_syus_system_user_sype_system_permission").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_syus_system_user_sype_system_permission");

        builder
            .HasKey(t => new {t.SystemUserId, t.SystemPermissionId})
            .HasName("rl_syus_system_user_sype_system_permission");
        builder.InsertSeedData($"{SeedJsonBasePath}.system-user-permission.json");
    }
}
