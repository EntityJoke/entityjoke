using EntityJoke.Connection;
using EntityJoke.Core;
using EntityJoke.Mocks;

namespace EntityJoke.Process.Commands
{
    public class DataTableGeneratorFactory
    {
        internal static IDataTableGenerador Get(string sql)
        {
            if (DictionaryInstanceFactory.IsDataTableGeneratorMock())
                return new DataTableGeneratorMock(sql);
            return new DataTableGenerator(sql);
        }
    }
}
