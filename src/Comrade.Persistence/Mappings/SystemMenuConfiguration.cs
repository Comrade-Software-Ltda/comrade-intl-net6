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
        builder.HasOne(x => x.Father)
            .WithMany(x => x.Childrens)
            .HasForeignKey(x => x.FatherId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        builder.InsertSeedData($"{SeedJsonBasePath}.system-menu.json");
    }
}