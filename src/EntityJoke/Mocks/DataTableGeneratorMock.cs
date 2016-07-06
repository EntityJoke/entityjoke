using EntityJoke.Core;
using EntityJoke.Process.Commands;
using System.Collections.Generic;

namespace EntityJoke.Mocks
{
    public class DataTableGeneratorMock : IDataTableGenerator
    {
        private readonly string sql;

        public DataTableGeneratorMock(string sql)
        {
            this.sql = sql;
        }

        public List<Dictionary<string, object>> Generate()
        {
            return DictionaryInstanceFactory.GetDataTable();
        }

    }
}
