using Comrade.Domain.Models;

namespace Comrade.Persistence.Mappings;

public class SystemUserConfiguration : IEntityTypeConfiguration<SystemUser>
{
    public void Configure(EntityTypeBuilder<SystemUser> builder)
    {
        builder.Property(b => b.Id).HasColumnName("ussi_sq_usuario_sistema").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_ussi_usuario_sistema");

        builder.HasIndex(c => c.Email).HasDatabaseName("ix_un_ussi_tx_email").IsUnique();
        builder.HasIndex(c => c.Registration).HasDatabaseName("ix_un_ussi_tx_matricula")
            .IsUnique();
    }
}