using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.Extensions;

namespace Comrade.Persistence.Mappings;

public class AirplaneConfiguration : BaseMappingConfiguration<Airplane>
{
    public override void Configure(EntityTypeBuilder<Airplane> builder)
    {
        builder.Property(b => b.Id).HasColumnName("airp_uuid_airplane").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_airp_airplane");
        builder.HasIndex(c => c.Code).HasDatabaseName("ix_un_airp_tx_code").IsUnique();
        builder.InsertSeedData($"{SeedJsonBasePath}.airplane.json");
    }
}
