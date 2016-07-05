using EntityJoke.Connection;
using System.Collections.Generic;
using System.Data.Common;

namespace EntityJoke.Process.Commands
{
    public class DataTableGenerator : IDataTableGenerator
    {
        private readonly string commandSql;

        public DataTableGenerator(string commandSql)
        {
            this.commandSql = commandSql;
        }

        public List<Dictionary<string, object>> Generate()
        {
            var conn = DbConnectionFactory.Get();
            conn.Open();

            var dataTable = GenerateDataTable(conn);

            conn.Close();

            return dataTable;
        }

        private List<Dictionary<string, object>> GenerateDataTable(DbConnection conn)
        {
            var adp = new DbDataAdapterFactory(commandSql, conn).Get();
            var dataTable = new List<Dictionary<string, object>>();
            return dataTable;
        }
    }
}
