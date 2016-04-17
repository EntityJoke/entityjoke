using EntityJoke.Core;
using System.Collections.Generic;
using System.Data;

namespace EntityJoke.Mocks
{
    public class SQLCommandExecutorMock<T> : ISQLCommandExecutor<T>
    {
        private string sql;

        public SQLCommandExecutorMock(string sql)
        {
            this.sql = sql;
        }

        public List<T> Execute()
        {
            DataTable dataTable = DictionaryInstanceFactory.GetDataTable();
            return LoadEntities(dataTable);
        }

        private static List<T> LoadEntities(DataTable dataTable)
        {
            return new EntitiesLoader<T>(dataTable).Load();
        }

    }
}
