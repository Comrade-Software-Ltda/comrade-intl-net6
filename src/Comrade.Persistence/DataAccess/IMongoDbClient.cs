using Comrade.Application.Bases;

namespace Comrade.Persistence.DataAccess
{
    public interface IMongoDbClient
    {
        IQueryable<T> Get<T>() where T : EntityDto;

        void InsertOne<T>(T obj) where T : EntityDto;

        void ReplaceOne<T>(T obj) where T : EntityDto;

        void DeleteOne<T>(int id) where T : EntityDto;
    }
}