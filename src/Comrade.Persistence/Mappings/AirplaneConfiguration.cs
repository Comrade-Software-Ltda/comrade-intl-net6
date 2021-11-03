using Comrade.Domain.Models;

namespace Comrade.Persistence.Mappings;

public class AirplaneConfiguration : IEntityTypeConfiguration<Airplane>
{
    public void Configure(EntityTypeBuilder<Airplane> builder)
    {
        builder.Property(b => b.Id).HasColumnName("airp_uuid_airplane").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_airp_airplane");

        builder.HasIndex(c => c.Code).HasDatabaseName("ix_un_airp_tx_code").IsUnique();
    }
}