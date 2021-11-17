using Sedc.Server.Core;

namespace Sedc.Server.TryTwo.MsSql
{
    internal class SqlServerFactory
    {
        internal static Func<IResponse> GetAction(IEnumerable<string> path)
        {
            /*
             * (after stripping the mssql prefix)
             * 
                /:database-name - list tables
                /table - error
                /table/:table-name - list columns
                /table/:table-name/data - show data
                /table/:table-name/:anything-else - error
            */


            var config = new JsonConfiguration("app-config.json");
            var connectionString = config.GetConnectionString("database");
            var pathArray = path.ToArray();

            var first = path.FirstOrDefault();

            if (string.IsNullOrEmpty(first)) {
                return () => new TextResponse { Message = "Hello, I'm an MS SQL Server processor. Usage info is in the code :)" };
            }

            if (first != "table")
            {
                var dbName = first;
                return () =>
                {
                    var dbLister = new SqlDatabaseLister { ConnectionString = connectionString};
                    var tables = dbLister.GetTableNames(dbName);
                    return new JsonResponse<IEnumerable<string>>
                    {
                        Message = tables
                    };
                };
            }

            if (pathArray.Length == 1)
            {
                return () => new TextResponse { Message = "MS SQL Server processor Error.", Status = Status.BadRequest };
            }
            var tableName = pathArray[1];

            if (pathArray.Length == 2) {
                // list columns for a table
                return () =>
                {
                    var dbLister = new SqlTableLister { ConnectionString = connectionString };
                    var columns = dbLister.GetColumnNames(tableName);
                    return new JsonResponse<IEnumerable<string>>
                    {
                        Message = columns
                    };
                };
            } 
            if (pathArray[2] != "data")
            {
                return () => new TextResponse { Message = "MS SQL Server processor Error.", Status = Status.BadRequest };
            }

            // get data for a table
            return () =>
            {
                var dbLister = new SqlTableLister { ConnectionString = connectionString };
                var data = dbLister.GetTableData(tableName);
                return new JsonResponse<IEnumerable<object>>
                {
                    Message = data
                };
            };

        }
    }
}