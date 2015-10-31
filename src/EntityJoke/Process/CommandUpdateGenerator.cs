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
        object objectUpdate;
        Entity entity;

        public CommandUpdateGenerator(object objectInsert)
        {
            this.objectUpdate = objectInsert;
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
                .Where(f => !f.IsKey && HasChange(f))
                .OrderBy(f => f.Name);
        }

        private bool HasChange(Field field)
        {
            return field.IsEntity ? GetJoinComparison(field) : GetFieldComparison(field);
        }

        private bool GetJoinComparison(Field field)
        {
            return true;
        }

        private bool GetFieldComparison(Field field)
        {
            var aspect = DictionaryEntitiesAspects.GetInstance().GetAspect(objectUpdate);

            var valueA = new ValueFieldExtractor(objectUpdate, field).Extract();
            var valueB = new ValueFieldExtractor(aspect, field).Extract();

            return valueA != valueB;
        }

        private string GetWhere()
        {
            Field idField = entity.FieldDictionary["id"];
            return String.Format("WHERE id = {0}", GetValueToUpdate(idField));
        }

        private string GetValueToUpdate(Field field)
        {
            if(!field.IsEntity)
                return new ValueFieldFormatted(objectUpdate, field).Format();

            return GetJoinIdValue(field);
        }

        private string GetJoinIdValue(Field field)
        {
            object join = new ValueFieldExtractor(objectUpdate, field).Extract();

            Entity entityJoin = DictionaryEntitiesMap.INSTANCE.GetEntity(join.GetType());
            Field idField = entityJoin.FieldDictionary["id"];

            return new ValueFieldFormatted(join, idField).Format();
        }

    }
}

