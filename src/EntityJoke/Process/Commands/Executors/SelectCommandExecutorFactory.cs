using EntityJoke.Core;
using EntityJoke.Mocks;

namespace EntityJoke.Process.Commands
{
    internal class SQLCommandExecutorFactory<T>
    {

        internal static SQLCommandExecutor<T> Get(string sql)
        {
            return new SQLCommandExecutor<T>(sql);
        }

    }
}
