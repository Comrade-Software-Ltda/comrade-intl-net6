#region

using Comrade.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Comrade.Persistence.Mappings;

public class AirplaneConfiguration : IEntityTypeConfiguration<Airplane>
{
    public void Configure(EntityTypeBuilder<Airplane> builder)
    {
        builder.Property(b => b.Id).HasColumnName("airp_sq_airplane").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_airp_airplane");

        builder.HasIndex(c => c.Code).HasDatabaseName("ix_un_airp_tx_codigo").IsUnique();
    }
}
