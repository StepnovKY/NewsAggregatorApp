using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public static class DatabaseHelper
{
    private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["NewsAggregator"].ConnectionString;

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(ConnectionString);
    }

    public static DataTable ExecuteQuery(string query)
    {
        using (var conn = GetConnection())
        {
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public static void ExecuteNonQuery(string query, SqlParameter[] parameters = null)
    {
        using (var conn = GetConnection())
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            cmd.ExecuteNonQuery();
        }
    }
}