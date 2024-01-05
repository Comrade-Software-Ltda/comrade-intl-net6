namespace Comrade.Persistence.ADO;

public class DbOto(string? databaseName, short dbId, DateTime creationDate)
{
    public DbOto() : this("", 0, new DateTime())
    {
    }


    public string? DatabaseName { get; set; } = databaseName;
    public short DbID { get; set; } = dbId;
    public DateTime CreationDate { get; set; } = creationDate;
}
