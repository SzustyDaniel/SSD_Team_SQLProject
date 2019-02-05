using Infrastructure;
using Infrastructure.Models;
using SwimmerManagmentUI.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwimmerManagmentUI.ViewModels
{
    public static class SqlClientHelper
    {
        private static readonly Dictionary<Type, string> typeToQuery = new Dictionary<Type, string>
        {
            {typeof(Coach),QueriesManager.GetAllCoaches},
            {typeof(RegularSwimmer),QueriesManager.GetAllCoaches},
            {typeof(StupidClass),QueriesManager.GetAllCoaches },
            {typeof(Team),QueriesManager.GetAllCoaches }
        };

        public static async Task<List<T>> Get<T>() where T : new()
        {
            if (typeToQuery.TryGetValue(typeof(T), out string query))
            {
                var connectionString = SqlConnectionStringHelper.ConnectionStringInAppConfig;
                return await SqlHelper.GetAllRowsFromDbAsync<T>(connectionString, query).ConfigureAwait(false);
            }
            else
            {
                throw new Exception("Query not found");
            }
        }
    }
}
