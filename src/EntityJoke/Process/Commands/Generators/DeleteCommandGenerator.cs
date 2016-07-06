using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System;

namespace EntityJoke.Process.Commands
{
    public class DeleteCommandGenerator
    {
        readonly Entity entity;
        readonly object objectDelete;

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
            return $"DELETE FROM {entity.Name} WHERE {GetWhere()}";
        }

        private string GetWhere()
        {
            return $"id = {GetValueToDelete()}";
        }

        private string GetValueToDelete()
        {
            var idField = entity.FieldDictionary["id"];
            return new FieldValueFormater(objectDelete, idField).Format();
        }
    }
}
