using EntityJoke.Connection;
using System.Data;
using System.Data.Common;

namespace EntityJoke.Process.Commands
{
    public class DataTableGenerator : IDataTableGenerator
    {
        private string commandSql;

        public DataTableGenerator(string commandSql)
        {
            this.commandSql = commandSql;
        }

        public DataTable Generate()
        {
            var conn = DbConnectionFactory.Get();
            conn.Open();

            var adp = new DbDataAdapterFactory(commandSql, conn).Get();
            var dataTable = new DataTable();

            adp.Fill(dataTable);

            conn.Close();

            return dataTable;
        }
    }
}
