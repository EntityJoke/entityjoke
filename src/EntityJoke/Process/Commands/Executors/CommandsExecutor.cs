using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using System.Data;

namespace EntityJoke.Process.Commands.Executors
{
    public abstract class CommandsExecutor
    {
        protected readonly object obj;
        protected readonly Entity entity;
        protected DataTable dataTable;

        protected ICommandSQLGenerator commandGenerator;
        protected IDataTableGenerator dataTableGenerator;

        protected CommandsExecutor(object obj)
        {
            this.obj = obj;
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType());
        }

        abstract public void Execute();
    }
}
