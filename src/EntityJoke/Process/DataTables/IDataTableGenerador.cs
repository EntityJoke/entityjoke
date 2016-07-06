using System.Collections.Generic;

namespace EntityJoke.Process.Commands
{
    public interface IDataTableGenerator
    {

        List<Dictionary<string, object>> Generate();

    }
}