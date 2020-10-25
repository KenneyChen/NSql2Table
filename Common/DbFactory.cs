using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace NSql2Table.Common
{
    public static class DbFactory
    {
        public static IDbConnection GetConnection(this DbType type, string connectionString)
        {
            switch (type)
            {
                case DbType.Mysql:
                    return new MySqlConnection(connectionString);
                case DbType.MSSQL:
                    return new SqlConnection(connectionString);
                default:
                    return new SqlConnection(connectionString);
            }
        }
    }
    public enum DbType
    {
        Mysql = 2,
        MSSQL = 1,
    }
}
