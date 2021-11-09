namespace Comrade.Persistence.DataAccess;

public interface IMongoDbContextSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}