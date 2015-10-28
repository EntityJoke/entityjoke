using EntityJoke.Connection;
using EntityJoke.Core;
using EntityJoke.Structure;
using EntityJokeTests.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Process
{
    public class NonQueryCommandsExecutor
    {
        protected object objectCommand;
        protected Entity entity;
        private string commandSQL;

        public NonQueryCommandsExecutor(object obj)
        {
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType());
            this.objectCommand = obj;
        }

        public void Execute()
        {
            PreExecute();

            this.commandSQL = GetCommandSQL();

            DataTable returnCommand = new DataTableGenerator(commandSQL).Generate();
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
            return IsNewObject() ? GetCommandInsert() : GetCommandUpdate();
        }

        private string GetCommandInsert()
        {
            return new CommandInsertGenerator(objectCommand).GetCommand() + " RETURNING ID";
        }

        private string GetCommandUpdate()
        {
            return new CommandUpdateGenerator(objectCommand).GetCommand() + " RETURNING ID";
        }

        private bool IsNewObject()
        {
            var idField = entity.FieldDictionary["id"];
            int id = (int)new ValueFieldExtractor(objectCommand, idField).Extract();
            return id == 0;
        }

        private void ProcessJoin(Field field)
        {
            var objectJoin = new ValueFieldExtractor(objectCommand, field).Extract();
            new NonQueryCommandsExecutor(objectJoin).Execute();
        }

        protected virtual void RefreshObject(DataTable returnCommand)
        {
            foreach (DataRow asv in returnCommand.Rows)
                new FieldValueSetter(objectCommand, entity.FieldDictionary["id"], asv["id"]).Set();
        }
    }
}
