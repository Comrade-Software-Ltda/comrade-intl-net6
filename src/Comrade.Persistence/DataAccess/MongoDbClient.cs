using Comrade.Application.Bases;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Comrade.Persistence.DataAccess
{
    public class MongoDbClient : IMongoDbClient
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbClient(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
            _mongoDatabase = client.GetDatabase("auth-user-service");
        }

        private IMongoCollection<T> GetCollection<T>()
            => _mongoDatabase.GetCollection<T>(nameof(T));

        public IQueryable<T> Get<T>() where T : EntityDto
            => GetCollection<T>().AsQueryable();

        public void InsertOne<T>(T obj) where T : EntityDto
            => GetCollection<T>().InsertOne(obj);

        public void ReplaceOne<T>(T obj) where T : EntityDto
            => GetCollection<T>().ReplaceOne(x => x.Id.Equals(obj.Id), obj);

        public void DeleteOne<T>(int id) where T : EntityDto
            => GetCollection<T>().DeleteOne(x => x.Id.Equals(id));
    }
}