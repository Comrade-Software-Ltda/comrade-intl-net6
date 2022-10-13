using System.Data;
using Microsoft.Data.SqlClient;

namespace Comrade.Persistence.ADO;

public class CreateDatabase
{
    public void Execute(string databaseName)
    {
        var myConn =
            new SqlConnection("Server=(localdb)\\mssqllocaldb;Trusted_Connection=True;MultipleActiveResultSets=true");

        var str = "CREATE DATABASE " + databaseName + " ON PRIMARY " +
                  "(NAME = MyDatabase_Data, " +
                  "FILENAME = 'C:\\oto\\MyDatabaseData.mdf', " +
                  "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                  "LOG ON (NAME = MyDatabase_Log, " +
                  "FILENAME = 'C:\\oto\\MyDatabaseLog.ldf', " +
                  "SIZE = 1MB, " +
                  "MAXSIZE = 5MB, " +
                  "FILEGROWTH = 10%)";

        var myCommand = new SqlCommand(str, myConn);
        try
        {
            myConn.Open();
            myCommand.ExecuteNonQuery();
        }
        finally
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
        }
    }
}
