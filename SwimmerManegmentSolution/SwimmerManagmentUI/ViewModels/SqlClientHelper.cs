using Infrastructure;
using Infrastructure.Models;
using SwimmerManagmentUI.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SwimmerManagmentUI.ViewModels
{
    public static class SqlClientHelper
    {
        private static readonly Dictionary<Type, string> typeToQuery = new Dictionary<Type, string>
        {
            {typeof(Coach),QueriesManager.GetAllCoaches},
            {typeof(RegularSwimmer),QueriesManager.SwimmersWithoutTraining},
            {typeof(StupidClass),QueriesManager.PotentialsContactAndByGender },
            {typeof(Team),QueriesManager.GetTeamsForCoach }
        };

        public static async Task<List<T>> Get<T>(params SqlParameter[] parameters) where T : new()
        {
            if (typeToQuery.TryGetValue(typeof(T), out string query))
            {
                var connectionString = SqlConnectionStringHelper.ConnectionStringInAppConfig;
                return await SqlHelper.GetAllRowsFromDbAsync<T>(connectionString, query, parameters).ConfigureAwait(false);
            }
            else
            {
                throw new KeyNotFoundException("Query not found");
            }
        }

        public static async Task<List<T>> Get<T>() where T : new()
        {
            if (typeToQuery.TryGetValue(typeof(T), out string query))
            {
                var connectionString = SqlConnectionStringHelper.ConnectionStringInAppConfig;
                return await SqlHelper.GetAllRowsFromDbAsync<T>(connectionString, query).ConfigureAwait(false);
            }
            else
            {
                throw new KeyNotFoundException("Query not found");
            }
        }
    }
}
