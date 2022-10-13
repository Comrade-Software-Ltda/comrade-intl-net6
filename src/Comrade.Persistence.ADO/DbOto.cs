namespace Comrade.Persistence.ADO;

public class DbOto
{
    public DbOto()
    {
        DatabaseName = "";
        DbID = 0;
    }


    public DbOto(string? databaseName, short dbId, DateTime creationDate)
    {
        DatabaseName = databaseName;
        DbID = dbId;
        CreationDate = creationDate;
    }

    public string? DatabaseName { get; set; }
    public short DbID { get; set; }
    public DateTime CreationDate { get; set; }
}
