using EntityJoke.Core;
using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Process
{
    public class DeleteCommandsExecutor : NonQueryCommandsExecutor
    {

        public DeleteCommandsExecutor(object objectDelete) : base(objectDelete){}

        protected override void PreExecute()
        {
            return;
        }

        protected override string GetCommandSQL()
        {
            return new CommandDeleteGenerator(objectCommand).GetCommand();
        }

        protected override void RefreshObject(System.Data.DataTable returnCommand)
        {
            entity.GetFields().ForEach(f => SetFieldNull(f));
        }

        private void SetFieldNull(Field field)
        {
            new FieldValueSetter(objectCommand, field, GetValue(field)).Set();
        }

        private object GetValue(Field field)
        {
            return IsNumber(field) ? (object)0 : null;
        }

        private bool IsNumber(Field field)
        {
            return field.Type.FullName.StartsWith("System.Int")
                || field.Type.FullName.StartsWith("System.Double");
        }

    }
}
