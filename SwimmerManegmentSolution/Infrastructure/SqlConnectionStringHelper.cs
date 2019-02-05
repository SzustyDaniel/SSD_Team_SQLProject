using Infrastructure.Models;
using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class SqlConnectionStringHelper
    {
        /// <summary>
        /// TODO : Retrieves connection string named "SingleConnection" from App.config. 
        /// Fast solution for home work. Remove later when connection window will be present.
        /// </summary>
        public static string ConnectionStringInAppConfig => ConfigurationManager.ConnectionStrings["SingleConnection"].ConnectionString;

        public static string CurrentServerName => GetBuilderFromStoredConnectionString()?.DataSource;

        public static string CurrentDbName => GetBuilderFromStoredConnectionString()?.InitialCatalog;

        public static List<DbName> AvailableDatabases(string serverName)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = serverName
            };
            string connString = builder.ConnectionString;
            return SqlHelper.GetAllRowsFromDb<DbName>(connString, QueriesManager.GetUserDatabases);
        }

        public static void SaveConnectionString(string serverName, string dbName)
        {
            throw new NotImplementedException();
        }

        private static SqlConnectionStringBuilder GetBuilderFromStoredConnectionString()
        {
            ConnectionStringSettingsCollection connectionStringSettingsCollection = ConfigurationManager.ConnectionStrings;
            if (connectionStringSettingsCollection.Count == 0) return null;
            string connectionString = connectionStringSettingsCollection[0].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            return builder;
        }
    }
}
