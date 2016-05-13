using System.Data;

namespace EntityJoke.Process.Commands
{
    public interface IDataTableGenerator
    {

        DataTable Generate();

    }
}