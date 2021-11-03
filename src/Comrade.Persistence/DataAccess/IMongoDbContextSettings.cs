namespace Comrade.Persistence.DataAccess;

public interface IMongoDbContextSettings
{
    string BooksCollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}