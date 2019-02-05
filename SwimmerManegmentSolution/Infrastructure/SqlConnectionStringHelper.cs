using Infrastructure.Models;
using Infrastructure.Queries;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Infrastructure
{
    public static class SqlConnectionStringHelper
    {
        private const string ConnectionStringKey = "SingleConnection";

        /// <summary>
        /// TODO : Retrieves connection string named "SingleConnection" from App.config. 
        /// Fast solution for home work. Remove later when connection window will be present.
        /// </summary>
        public static string ConnectionStringInAppConfig => ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;

        public static string CurrentServerName => GetBuilderFromStoredConnectionString()?.DataSource;

        public static string CurrentDbName => GetBuilderFromStoredConnectionString()?.InitialCatalog;

        public static List<DbName> AvailableDatabases(string dataSource)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = dataSource
            };
            string connString = builder.ConnectionString;
            return SqlHelper.GetAllRowsFromDb<DbName>(connString, QueriesManager.GetUserDatabases);
        }

        public static void SaveConnectionString(string dataSource, string dbName)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = dataSource,
                InitialCatalog = dbName
            };
            ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString = builder.ConnectionString;
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
