using EntityJoke.Connection;
using EntityJoke.Core;
using EntityJoke.Mocks;

namespace EntityJoke.Process.Commands
{
    public class DataTableGeneratorFactory
    {
        public static IDataTableGenerator Get(string sql)
        {
            if (DictionaryInstanceFactory.IsDataTableGeneratorMock())
                return new DataTableGeneratorMock(sql);
            return new DataTableGenerator(sql);
        }
    }
}
