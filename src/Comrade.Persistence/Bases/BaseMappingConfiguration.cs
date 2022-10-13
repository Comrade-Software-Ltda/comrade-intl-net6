using Comrade.Domain.Bases;

namespace Comrade.Persistence.Bases;

public abstract class BaseMappingConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    protected const string SeedJsonBasePath = "Comrade.Persistence.SeedData";
    public abstract void Configure(EntityTypeBuilder<T> builder);
}
