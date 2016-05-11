using EntityJoke.Connection;
using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace EntityJoke.Process.Commands
{
    public class NonQueryCommandsExecutor
    {
        private DataTable returnCommand;
        private ICommandSQLGenerator commandGenerator;
        private string commandSQL;

        protected Entity entity;
        protected object objectCommand;

        public NonQueryCommandsExecutor(object obj)
        {
            commandGenerator = CommandSQLGenerator.NewInstance(obj);
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType());
            this.objectCommand = obj;
        }

        public void Execute()
        {
            PreExecute();

            commandSQL = GetCommandSQL();

            if (commandSQL == "")
                return;

            Debug.WriteLine("CommandExecutor : " + commandSQL);

            returnCommand = new DataTableGenerator(commandSQL).Generate();
            RefreshObject(returnCommand);
        }

        protected virtual void PreExecute()
        {
            entity.GetFields()
                .Where(f => f.IsEntity).ToList()
                .ForEach(j => ProcessJoin(j));
        }

        protected virtual string GetCommandSQL()
        {
            return commandGenerator.Generate();
        }

        private void ProcessJoin(Field field)
        {
            var objectJoin = field.GetExtractor(objectCommand).Extract();

            if (objectJoin == null)
                return;

            new NonQueryCommandsExecutor(objectJoin).Execute();
        }

        protected virtual void RefreshObject(DataTable returnCommand)
        {
            foreach (DataRow row in returnCommand.Rows)
                entity.FieldDictionary["id"].GetSetter(objectCommand, row["id"]).Set();
        }
    }
}
