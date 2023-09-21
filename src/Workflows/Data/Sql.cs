using System.Data;
using Microsoft.Data.SqlClient;

namespace Bis.ApplySevenDayProcessAutomation.Workflows.Data;

public class Sql
{
    public static T ExecuteScalar<T>(string connectionString, string sql, int commandTimeout = 30)
    {
        using var connection = new SqlConnection(connectionString);
        var command = new SqlCommand(sql, connection) {CommandTimeout = commandTimeout};

        connection.Open();
        return (T) Convert.ChangeType(command.ExecuteScalar(), typeof(T));
    }
    
    public static IEnumerable<DataRow> ExecuteSp(string spName, string connectionString)
    {
        var dataSet = new DataSet();

        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand(spName, connection);

            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 300;
            connection.Open();
            new SqlDataAdapter(command).Fill(dataSet);
            return dataSet.Tables[0].Rows.Cast<DataRow>();
        }
    }
}