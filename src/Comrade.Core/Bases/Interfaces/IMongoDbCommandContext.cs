using Comrade.Domain.Bases;

namespace Comrade.Core.Bases.Interfaces;

public interface IMongoDbCommandContext
{
    void InsertOne<T>(T obj) where T : Entity;

    void ReplaceOne<T>(T obj) where T : Entity;

    void DeleteOne<T>(Guid id) where T : Entity;
}