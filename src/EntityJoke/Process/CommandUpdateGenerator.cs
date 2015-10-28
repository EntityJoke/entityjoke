using EntityJoke.Core;
using EntityJoke.Process;
using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityJokeTests.Core
{
    public class CommandUpdateGenerator
    {
        object objectInsert;
        Entity entity;

        public CommandUpdateGenerator(object objectInsert)
        {
            this.objectInsert = objectInsert;
            this.entity = DictionaryEntitiesMap.INSTANCE.GetEntity(objectInsert.GetType());
        }

        public string GetCommand()
        {
            return GetInsertThisObject();
        }

        private string GetInsertThisObject()
        {
            return String.Format("{0} {1} {2}",
                GetUpdate(),
                GetColumns(),
                GetWhere());
        }

        private string GetUpdate()
        {
            return String.Format("UPDATE {0}", entity.Name);
        }

        private string GetColumns()
        {
            string columns = "";

            foreach (Field field in GetFieldsOrdered())
                columns += String.Format(", {0} = {1}", field.ColumnName, GetValueToUpdate(field));

            return String.Format("SET {0}", columns.Substring(2));
        }

        private IEnumerable<Field> GetFieldsOrdered()
        {
            return entity.GetFields()
                .Where(f => !f.IsKey)
                .OrderBy(f => f.Name);
        }

        private string GetWhere()
        {
            Field idField = entity.FieldDictionary["id"];
            return String.Format("WHERE id = {0}", GetValueToUpdate(idField));
        }

        private string GetValueToUpdate(Field field)
        {
            if(!field.IsEntity)
                return new ValueFieldFormatted(objectInsert, field).Format();

            return GetJoinIdValue(field);
        }

        private string GetJoinIdValue(Field field)
        {
            object join = new ValueFieldExtractor(objectInsert, field).Extract();

            Entity entityJoin = DictionaryEntitiesMap.INSTANCE.GetEntity(join.GetType());
            Field idField = entityJoin.FieldDictionary["id"];

            return new ValueFieldFormatted(join, idField).Format();
        }

    }
}

