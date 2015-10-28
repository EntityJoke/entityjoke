using EntityJoke.Core;
using EntityJoke.Process;
using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityJokeTests.Core
{
    public class CommandInsertGenerator
    {
        object objectInsert;
        Entity entity;

        public CommandInsertGenerator(object objectInsert)
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
            return String.Format("{0} {1} VALUES {2}",
                GetInsert(),
                GetColumns(),
                GetValues());
        }

        private string GetInsert()
        {
            return String.Format("INSERT INTO {0}", entity.Name);
        }

        private string GetColumns()
        {
            string columns = "";

            foreach (Field field in GetFieldsOrdered())
                columns += String.Format(", {0}", field.ColumnName);

            return String.Format("({0})", columns.Substring(2));
        }

        private IEnumerable<Field> GetFieldsOrdered()
        {
            return entity.GetFields()
                .Where(f => !f.IsKey)
                .OrderBy(f => f.Name);
        }

        private string GetValues()
        {
            string values = "";

            foreach (Field field in GetFieldsOrdered())
                values += String.Format(", {0}", GetValueToInsert(field));

            return String.Format("({0})", values.Substring(2));
        }

        private string GetValueToInsert(Field field)
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

