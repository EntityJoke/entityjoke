using EntityJoke.Core;
using EntityJoke.Process.Commands;
using System.Data;

namespace EntityJoke.Mocks
{
    public class DataTableGeneratorMock : IDataTableGenerator
    {
        private readonly string sql;

        public DataTableGeneratorMock(string sql)
        {
            this.sql = sql;
        }

        public DataTable Generate()
        {
            return DictionaryInstanceFactory.GetDataTable();
        }

    }
}
