using System.Collections.Generic;

namespace EntityJoke.Core
{
    internal interface ISQLCommandExecutor<T>
    {

        List<T> Execute();

    }
}