using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedc.Server.TryTwo.MsSql
{
    internal class SqlTableLister
    {
        public string ConnectionString { get; init; }

        public List<string> GetColumnNames(string tableName)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = @"SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = @tableName";

            using var command = new SqlCommand(query, connection);
            command.Parameters.Add("tableName", System.Data.SqlDbType.NVarChar);
            command.Parameters["tableName"].Value = tableName;

            using var reader = command.ExecuteReader();

            var result = new List<string>();
            while (reader.Read())
            {
                string columnName = reader.GetString(0);
                result.Add(columnName);
            }

            return result;
        }

        public List<object[]> GetTableData(string tableName)
        {
            // not-good-enough-sanitation
            var dbLister = new SqlDatabaseLister { ConnectionString = ConnectionString };
            var tables = dbLister.GetTableNames("Books").Select(table => table.ToLowerInvariant());
            if (!tables.Contains(tableName.ToLowerInvariant()))
            {
                throw new SedcServerException($"Unknown table {tableName}");
            }

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = @$"SELECT * from {tableName}";

            using var command = new SqlCommand(query, connection);

            using var reader = command.ExecuteReader();

            var columns = reader.GetColumnSchema().Count;

            var result = new List<object[]>();
            while (reader.Read())
            {
                var values = new object[columns];
                reader.GetValues(values);
                result.Add(values);
            }

            return result;
        }

    }
}
