using EntityJoke.Connection;
using System.Data;
using System.Data.Common;

namespace EntityJoke.Process.Commands
{
    public class DataTableGenerator : IDataTableGenerador
    {
        private string commandSQL;

        public DataTableGenerator(string commandSQL)
        {
            this.commandSQL = commandSQL;
        }

        public DataTable Generate()
        {
            DbConnection conn = new DbConnectionFactory().Get();
            conn.Open();

            DbDataAdapter adp = new DbDataAdapterFactory(commandSQL, conn).Get();
            DataTable dataTable = new DataTable();

            adp.Fill(dataTable);

            conn.Close();

            return dataTable;
        }
    }
}
