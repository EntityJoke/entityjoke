using EntityJoke.Structure.Fields;

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
            return new CommandDeleteGenerator(objectCommand).GetCommand();
        }

        protected override void RefreshObject(System.Data.DataTable returnCommand)
        {
            entity.GetFields().ForEach(f => SetFieldNull(f));
        }

        private void SetFieldNull(Field field)
        {
            field.GetSetter(objectCommand, GetValue(field)).Set();
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
