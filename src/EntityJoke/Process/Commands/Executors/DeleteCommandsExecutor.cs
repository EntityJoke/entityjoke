using EntityJoke.Structure.Fields;
using System.Collections.Generic;

namespace EntityJoke.Process.Commands
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
            return new DeleteCommandGenerator(objectCommand).GetCommand();
        }

        protected override void RefreshObject(List<Dictionary<string, object>> returnCommand)
        {
            entity.GetFields().ForEach(f => SetFieldNull(f));
        }

        private void SetFieldNull(Field field)
        {
            field.GetSetter(objectCommand, GetValue(field)).Set();
        }

        private static object GetValue(Field field)
        {
            return IsNumber(field) ? (object)0 : null;
        }

        private static bool IsNumber(Field field)
        {
            return field.Type.FullName.StartsWith("System.Int")
                || field.Type.FullName.StartsWith("System.Double");
        }

    }
}
