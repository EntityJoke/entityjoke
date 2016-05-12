using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System;

namespace EntityJoke.Process.Commands
{
    public class DeleteCommandGenerator
    {
        object objectDelete;
        Entity entity;

        public DeleteCommandGenerator(object objectDelete)
        {
            this.objectDelete = objectDelete;
            this.entity = DictionaryEntitiesMap.INSTANCE.GetEntity(objectDelete.GetType());
        }

        public string GetCommand()
        {
            return GetDeleteThisObject();
        }

        private string GetDeleteThisObject()
        {
            return String.Format("DELETE FROM {0} WHERE {1}",
                entity.Name,
                GetWhere());
        }

        private string GetWhere()
        {
            Field idField = entity.FieldDictionary["id"];
            return String.Format("id = {0}", GetValueToDelete(idField));
        }

        private string GetValueToDelete(Field field)
        {
            return new FieldValueFormatted(objectDelete, field).Format();
        }
    }
}
