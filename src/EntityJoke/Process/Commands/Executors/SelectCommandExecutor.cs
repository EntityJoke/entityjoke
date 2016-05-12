using EntityJoke.Connection;
using EntityJoke.Core;
using EntityJoke.Core.Loaders;
using System.Collections.Generic;
using System.Data;

namespace EntityJoke.Process.Commands
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
            return DataTableGeneratorFactory.Get(commandSQL).Generate();
        }

        private static List<T> LoadEntities(DataTable dataTable)
        {
            return new EntitiesLoader<T>(dataTable).Load();
        }
    }
}
