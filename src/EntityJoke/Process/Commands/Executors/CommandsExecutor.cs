using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using System.Collections.Generic;

namespace EntityJoke.Process.Commands.Executors
{
    public abstract class CommandsExecutor
    {
        protected readonly object obj;
        protected readonly Entity entity;
        protected List<Dictionary<string, object>> dataTable;

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
