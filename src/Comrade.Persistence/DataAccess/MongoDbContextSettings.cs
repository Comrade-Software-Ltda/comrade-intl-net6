namespace Comrade.Persistence.DataAccess;

public class MongoDbContextSettings : IMongoDbContextSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}
