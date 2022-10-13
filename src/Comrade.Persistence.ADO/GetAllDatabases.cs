using System.Data;
using Microsoft.Data.SqlClient;

namespace Comrade.Persistence.ADO;

public class GetAllDatabases
{
    public List<DbOto> Execute()
    {
        var tes = new List<DbOto>();
        using (var con = new SqlConnection(
                   "Server=(localdb)\\mssqllocaldb;Trusted_Connection=True;MultipleActiveResultSets=true"))
        {
            con.Open();
            var databases = con.GetSchema("Databases");
            foreach (DataRow database in databases.Rows)
            {
                var oto = new DbOto();
                oto.DatabaseName = database.Field<string>("database_name");
                oto.DbID = database.Field<short>("dbid");
                oto.CreationDate = database.Field<DateTime>("create_date");
                tes.Add(oto);
            }
        }

        return tes;
    }
}
