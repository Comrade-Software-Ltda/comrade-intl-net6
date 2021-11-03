using Comrade.Domain.Bases.Interfaces;

namespace Comrade.Domain.Bases;

public abstract class Entity : IEntity
{
    [Key] public Guid Id { get; set; }

    public virtual Guid Key => Id;

    public virtual string Value => ToString()!;
}