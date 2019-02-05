using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class SqlHelper
    {
        public static List<T> GetAllRowsFromDb<T>(string connectionString, string query, params SqlParameter[] parameters) where T : new()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<T> list = new List<T>();
                PropertyInfo[] publicProperties = typeof(T).GetProperties();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);

                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    T row = new T();
                    foreach (PropertyInfo property in publicProperties)
                    {
                        object value = reader[property.Name];
                        property.SetValue(row, value is DBNull ? null : value);
                    }
                    list.Add(row);
                }
                
                return list;
            }
        }

        public async static Task<List<T>> GetAllRowsFromDbAsync<T>(string connectionString, string query) where T : new()
        {
            List<T> list = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader reader;
                try
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    SqlCommand command = new SqlCommand(query, connection);
                    reader = await command.ExecuteReaderAsync();
                }
                catch (Exception e)
                {
                    throw new Exception("Connection Error", e);
                }

                try
                {
                    PropertyInfo[] publicProperties = typeof(T).GetProperties();
                    while (await reader.ReadAsync())
                    {
                        T row = new T();
                        foreach (PropertyInfo property in publicProperties)
                        {
                            object value = reader[property.Name];
                            property.SetValue(row, value is DBNull ? null : value);
                        }
                        list.Add(row);
                    }
                    return list;
                }
                catch (Exception e)
                {
                    throw new Exception("Reader Error", e);
                }
            }
        }

        public static List<T> GetAllRowsFromDb<T>(string connectionString, string query) where T : new()
        {
            return GetAllRowsFromDb<T>(connectionString, query, null);
        }
    }
}
