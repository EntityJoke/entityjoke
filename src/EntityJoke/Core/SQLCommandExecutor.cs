using EntityJoke.Connection;
using System.Collections.Generic;
using System.Data;

namespace EntityJoke.Core
{
    public class SQLCommandExecutor<T>
    {
        private string commandSQL;

        public SQLCommandExecutor(string sql)
        {
            this.commandSQL = sql;
        }

        public List<T> Execute()
        {
            DataTable dataTable = ExecuteCommandSQL();
            return LoadEntities(dataTable);
        }

        private DataTable ExecuteCommandSQL()
        {
            return new DataTableGenerator(commandSQL).Generate();
        }

        private static List<T> LoadEntities(DataTable dataTable)
        {
            return new EntitiesLoader<T>(dataTable).Load();
        }
    }
}
