using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ConstructorPC.model
{
    class ConnectionFactory
    {
        private static string connStr;
        private static ConnectionFactory cf;
        private static DbType dbType;

        public static ConnectionFactory getInstance()
        {
            if (cf == null) return new ConnectionFactory();
            return cf;
        }

        public static ConnectionFactory newInstance(DbType type, string connectionString)
        {
            if (cf == null)
            {
                connStr = connectionString;
                dbType = type;
                cf = new ConnectionFactory(type, connectionString);
            }
            return cf;
        }

        private ConnectionFactory()
        {
            connStr = "server=localhost;database=pc_components;port=3306;user=root;password=1326;";
        }

        private ConnectionFactory(DbType type, string connectionString)
        {
            connStr = connectionString;
        }

        public DbConnection getConnection()
        {
            switch(dbType)
            {
                case DbType.MySQL:
                    return new MySqlConnection(connStr);
                case DbType.MSSQL:
                    return new SqlConnection(connStr);
                default:
                    return null;
            }        
        }

        public enum DbType
        {
            MySQL, MSSQL, SQLite
        }
    }
}
