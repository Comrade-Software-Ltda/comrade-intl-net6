using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MongoDB.Driver;

namespace Comrade.Persistence.DataAccess;

public class MongoDbContext : IMongoDbCommandContext, IMongoDbQueryContext
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoDbContext(IMongoDbContextSettings configuration)
    {
        var client =
            new MongoClient(configuration.ConnectionString);
        _mongoDatabase = client.GetDatabase(configuration.DatabaseName);
    }

    public void InsertOne<T>(T obj) where T : Entity
    {
        GetCollection<T>().InsertOne(obj);
    }

    public void ReplaceOne<T>(T obj) where T : Entity
    {
        GetCollection<T>().ReplaceOne(x => x.Id.Equals(obj.Id), obj,
            new ReplaceOptions() { IsUpsert = true });
    }

    public void DeleteOne<T>(Guid id) where T : Entity
    {
        GetCollection<T>().DeleteOne(x => x.Id.Equals(id));
    }

    private IMongoCollection<T> GetCollection<T>()
    {
        return _mongoDatabase.GetCollection<T>(nameof(T));
    }

    public IQueryable<T> GetAll<T>() where T : EntityDto
    {
        return GetCollection<T>().AsQueryable();
    }
}