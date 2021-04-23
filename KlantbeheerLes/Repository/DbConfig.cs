using Microsoft.Data.SqlClient;

namespace Repository
{
    public class DbConfig
    {
        #region Properties
        // Een eenvoudig singleton...
        private static SqlConnection _sqlConnection = null;
        public static SqlConnection Connection { get { if (_sqlConnection == null) { _sqlConnection = CreateConnection(); } return _sqlConnection; } }
        #endregion

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=StoreDb;Integrated Security=True");
        }
    }
}
