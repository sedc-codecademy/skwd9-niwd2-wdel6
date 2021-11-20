using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Microservice.SqlServerProcessor
{
    internal class SqlDatabaseLister
    {
        public string ConnectionString { get; init; }

        public List<string> GetTableNames(string database)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = @$"SELECT TABLE_NAME 
FROM {database}.INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
  AND TABLE_NAME<> 'sysdiagrams'";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            var result = new List<string>();
            while (reader.Read())
            {
                string tableName = reader.GetString(0);
                result.Add(tableName);
            }

            return result;
        }
    }
}
