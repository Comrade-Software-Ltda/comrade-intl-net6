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
            new ReplaceOptions {IsUpsert = true});
    }

    public void DeleteOne<T>(Guid id) where T : Entity
    {
        GetCollection<T>().DeleteOne(x => x.Id.Equals(id));
    }

    public IQueryable<T> GetAll<T>() where T : Entity
    {
        return GetCollection<T>().AsQueryable();
    }

    public async Task<T> GetById<T>(Guid id) where T : Entity?
    {
        var filter = Builders<T>.Filter;
        var eqFilter = filter.Eq(x => x.Id, id);

        var result = await GetCollection<T>().FindAsync<T>(eqFilter).ConfigureAwait(false);
        return await result.FirstOrDefaultAsync().ConfigureAwait(false);
    }

    private IMongoCollection<T> GetCollection<T>()
    {
        return _mongoDatabase.GetCollection<T>(nameof(T));
    }
}
