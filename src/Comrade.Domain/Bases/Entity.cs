using Comrade.Domain.Bases.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Comrade.Domain.Bases;

public abstract class Entity : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    [Key]
    public Guid Id { get; set; }

    public virtual Guid Key => Id;

    public virtual string Value => ToString()!;
}
