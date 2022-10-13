using Comrade.Domain.Models;
using Comrade.Persistence.Bases;
using Comrade.Persistence.Extensions;

namespace Comrade.Persistence.Mappings;

public class SystemMenuConfiguration : BaseMappingConfiguration<SystemMenu>
{
    public override void Configure(EntityTypeBuilder<SystemMenu> builder)
    {
        builder.Property(b => b.Id).HasColumnName("syme_uuid_system_menu").IsRequired();
        builder.HasKey(c => c.Id).HasName("pk_syme_system_menu");

        builder.HasMany(x => x.Submenus)
            .WithOne(x => x.Menu)
            .HasForeignKey(x => x.MenuId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.InsertSeedData($"{SeedJsonBasePath}.system-menu.json");
    }
}
