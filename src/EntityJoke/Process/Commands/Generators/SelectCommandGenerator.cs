using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;

namespace EntityJoke.Process.Commands
{
    public class SelectCommandGenerator
    {
        public static ICommandSQLGenerator NewInstance(object obj)
        {
            if(IsNewObject(obj))
                return new InsertCommandGenerator(obj);
            return new UpdateCommandGenerator(obj);
        }

        private static bool IsNewObject(object objectCommand)
        {
            return GetIdValue(objectCommand) == 0;
        }

        private static int GetIdValue(object objectCommand)
        {
            var idField = GetIdField(objectCommand);
            return (int)idField.GetExtractor(objectCommand).Extract();
        }

        private static Field GetIdField(object objectCommand)
        {
            return GetEntity(objectCommand).FieldDictionary["id"];
        }

        private static Entity GetEntity(object objectCommand)
        {
            return DictionaryEntitiesMap.INSTANCE.GetEntity(objectCommand.GetType());
        }
    }
}
