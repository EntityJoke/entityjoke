using EntityJoke.Structure.Entities;
using System.Data;

namespace EntityJoke.Process.Commands.Executors
{
    public class InsertCommandsExecutor : CommandsExecutor
    {
        public InsertCommandsExecutor(object obj)
            : base(obj)
        {
            commandGenerator = new InsertCommandGenerator(obj);
            dataTableGenerator = DataTableGeneratorFactory.Get(CommandSql());
        }

        private string CommandSql()
        {
            return commandGenerator.Generate();
        }

        public override void Execute()
        {
            dataTable = dataTableGenerator.Generate();
            RefreshObject();
        }

        protected virtual void RefreshObject()
        {
            foreach (var row in dataTable)
                entity.FieldDictionary["id"].GetSetter(obj, row["id"]).Set();
        }

    }
}
