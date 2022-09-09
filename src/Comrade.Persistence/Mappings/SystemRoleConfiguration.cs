using Comrade.Domain.Models;

namespace Comrade.Persistence.Mappings;

public class SystemRoleConfiguration : IEntityTypeConfiguration<SystemRole>
{
    public void Configure(EntityTypeBuilder<SystemRole> builder)
    {
        builder.Property(b => b.Id).HasColumnName("syro_uuid_system_role").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_syro_system_role");

        builder.HasMany(x => x.SystemUsers)
            .WithMany(x => x.SystemRoles)
            .UsingEntity(j => j.ToTable("syus_system_user_syro_system_role"));
    }
}
