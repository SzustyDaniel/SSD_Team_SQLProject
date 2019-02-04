using Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class SqlHelper
    {
        public static List<T> GetAllRowsFromDb<T>(string connectionString, string query) where T : new()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<T> list = new List<T>();
                PropertyInfo[] publicProperties = typeof(T).GetProperties();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                T row = new T();
                while (reader.Read())
                {
                    foreach (PropertyInfo property in publicProperties)
                    {
                        object value = reader[property.Name];
                        property.SetValue(row, value is DBNull ? null : value);
                    }
                }
                list.Add(row);
                return list;
            }
        }
    }
}
