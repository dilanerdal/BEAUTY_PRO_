using System.Data.SqlClient;

namespace BEAUTY_PRO_
{
    internal class DatabaseConnection
    {
        SqlConnection connection = new SqlConnection(
            "Server=ASUSVIVOBOOK\\SQLEXPRESS;Database=BEAUTY_PRO_DB;Trusted_Connection=True;"
        );

        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}