using Comrade.Domain.Models;

namespace Comrade.Persistence.Mappings;

public class SystemUserSystemRoleConfiguration : IEntityTypeConfiguration<SystemUserSystemRole>
{
    public void Configure(EntityTypeBuilder<SystemUserSystemRole> builder)
    {
        builder.Property(b => b.Id).HasColumnName("syus_uuid_system_user_syro_system_role").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_syus_system_user_syro_system_role");
        /*builder.HasOne(x => x.SystemUserId)
            .HasForeignKey(x => x.SystemUserId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);*/
    }
}