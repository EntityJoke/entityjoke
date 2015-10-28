using EntityJoke.Core;
using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Process
{
    public class CommandDeleteGenerator
    {
        object objectDelete;
        Entity entity;

        public CommandDeleteGenerator(object objectDelete)
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
            return new ValueFieldFormatted(objectDelete, field).Format();
        }
    }
}
