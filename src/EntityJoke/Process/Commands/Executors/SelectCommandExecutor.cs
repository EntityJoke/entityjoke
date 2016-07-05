using EntityJoke.Core.Loaders;
using System.Collections.Generic;

namespace EntityJoke.Process.Commands
{
    public class SQLCommandExecutor<T>
    {
        private readonly string commandSQL;

        public SQLCommandExecutor(string sql)
        {
            commandSQL = sql;
        }

        public List<T> Execute()
        {
            var dataTable = ExecuteCommandSQL();
            return LoadEntities(dataTable);
        }

        private List<Dictionary<string, object>> ExecuteCommandSQL()
        {
            return DataTableGeneratorFactory.Get(commandSQL).Generate();
        }

        private static List<T> LoadEntities(List<Dictionary<string, object>> dataTable)
        {
            return new EntitiesLoader<T>(dataTable).Load();
        }
    }
}
