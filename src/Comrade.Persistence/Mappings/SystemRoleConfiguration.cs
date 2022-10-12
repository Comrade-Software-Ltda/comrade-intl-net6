using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.Extensions;

namespace Comrade.Persistence.Mappings;

public class SystemRoleConfiguration : BaseMappingConfiguration<SystemRole>
{
    public override void Configure(EntityTypeBuilder<SystemRole> builder)
    {
        builder.Property(b => b.Id).HasColumnName("syro_uuid_system_role").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_syro_system_role");
        builder.InsertSeedData($"{SeedJsonBasePath}.system-role.json");
    }
}
