using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.Extensions;

namespace Comrade.Persistence.Mappings;

public class SystemUserRoleConfiguration : BaseMappingConfiguration<SystemUser>
{
    public override void Configure(EntityTypeBuilder<SystemUser> builder)
    {
        builder
            .HasMany(p => p.SystemRoles)
            .WithMany(p => p.SystemUsers)
            .UsingEntity<SystemUserSystemRole>(BuildSystemUserSystemRoleRelationship);
    }

    private static void BuildSystemUserSystemRoleRelationship(EntityTypeBuilder<SystemUserSystemRole> builder)
    {
        builder
            .HasOne(pt => pt.SystemRole)
            .WithMany(t => t.SystemUserRoles)
            .HasForeignKey(pt => pt.SystemRoleId);
        builder
            .HasOne(pt => pt.SystemUser)
            .WithMany(p => p.SystemUserRoles)
            .HasForeignKey(pt => pt.SystemUserId);

        builder.Property(b => b.Id).HasColumnName("pk_uuid_syus_system_user_syro_system_role").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_syus_system_user_syro_system_role");

        builder
            .HasKey(t => new {t.SystemUserId, t.SystemRoleId})
            .HasName("rl_syus_system_user_syro_system_role");

        builder.InsertSeedData($"{SeedJsonBasePath}.system-user-role.json");
    }
}
