using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Repository
{
    public static class DbConfig // static: nooit een instantie; dwz geen ctor
    {
        #region Properties
        // Een eenvoudig singleton... "DESIGN PATTERN" SOLID
        private static SqlConnection _sqlConnection = null;

        // Instantie van SqlConnection wordt LAZY gemaakt: op allerlaatste moment aanmaken
        // "Een" keer aangemaakt => "single-"ton
        public static SqlConnection Connection { get { if (_sqlConnection == null) { _sqlConnection = CreateConnection(); } return _sqlConnection; } /* geen set: zo controleren we toegang tot de instantie */ }
        #endregion

        // Indien ik toch een aparte extra connectie wil, gebruik ik de volgende public method:
        public static SqlConnection CreateConnection()
        {
            var s = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            // Wanneer we niets vinden in de app.config file, dan kiezen we voor een default waarde:
            if (string.IsNullOrEmpty(s))
                s = @"Data Source=.\SQLEXPRESS;Initial Catalog=StoreDb;Integrated Security=True";
            return new SqlConnection(s);
        }
    }
}
