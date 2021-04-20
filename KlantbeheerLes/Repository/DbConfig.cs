using Microsoft.Data.SqlClient;

namespace Repository
{
    public class DbConfig
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=StoreDb;Integrated Security=True");
        }
    }
}
