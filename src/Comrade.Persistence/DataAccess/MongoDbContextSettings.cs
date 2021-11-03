namespace Comrade.Persistence.DataAccess;

public class MongoDbContextSettings : IMongoDbContextSettings
{
    public string BooksCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}

