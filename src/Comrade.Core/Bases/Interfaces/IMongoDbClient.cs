using Comrade.Domain.Bases;

namespace Comrade.Core.Bases.Interfaces
{
    public interface IMongoDbClient
    {
        void InsertOne<T>(T obj) where T : Entity;

        void ReplaceOne<T>(T obj) where T : Entity;

        void DeleteOne<T>(int id) where T : Entity;
    }
}