using EntityJoke.Mocks;

namespace EntityJoke.Core
{
    internal class SQLCommandExecutorFactory<T>
    {

        internal static ISQLCommandExecutor<T> Get(string sql)
        {
            if (DictionaryInstanceFactory.IsSQLCommandExecutorMock())
                return new SQLCommandExecutorMock<T>(sql);
            return new SQLCommandExecutor<T>(sql);
        }

    }
}
