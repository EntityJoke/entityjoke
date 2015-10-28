using System.Data;
using System.Data.Common;

namespace EntityJoke.Connection
{
    public class DataTableGenerator
    {
        private string commandSQL;

        public DataTableGenerator(string commandSQL)
        {
            this.commandSQL = commandSQL;
        }

        internal DataTable Generate()
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
