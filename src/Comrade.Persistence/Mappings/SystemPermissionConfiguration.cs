using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.Extensions;

namespace Comrade.Persistence.Mappings;

public class SystemPermissionConfiguration : BaseMappingConfiguration<SystemPermission>
{
    public override void Configure(EntityTypeBuilder<SystemPermission> builder)
    {
        builder.Property(b => b.Id).HasColumnName("sype_uuid_system_permission").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_sype_system_permission");
        builder.InsertSeedData($"{SeedJsonBasePath}.system-permission.json");
    }
}
