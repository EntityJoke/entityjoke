using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityJoke.Process.Commands
{
    public class UpdateCommandGenerator : ICommandSQLGenerator
    {
        object objectUpdate;
        Entity entity;

        public UpdateCommandGenerator(object objectInsert)
        {
            this.objectUpdate = objectInsert;
            this.entity = DictionaryEntitiesMap.INSTANCE.GetEntity(objectInsert.GetType());
        }

        public string Generate()
        {
            return ObjectHasChange() ? GetUpdateThisObject() : "";
        }

        private bool ObjectHasChange()
        {
            return entity.GetFields().Any(f => HasChange(f));
        }

        private string GetUpdateThisObject()
        {
            return String.Format("{0} {1} {2} RETURNING ID",
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

            foreach (Field field in GetFieldsToUpdateOrdered())
                columns += String.Format(", {0} = {1}", field.ColumnName, GetValueToUpdate(field));

            return String.Format("SET {0}", columns.Length > 0 ? columns.Substring(2) : "");
        }

        private List<Field> GetFieldsToUpdateOrdered()
        {
            return entity.GetFields()
                .Where(f => !f.IsKey && HasChange(f))
                .OrderBy(f => f.Name).ToList();
        }

        private bool HasChange(Field field)
        {
            return field.IsEntity ? !IsEqualsJoinObjects(objectUpdate, field) : !IsEqualsField(objectUpdate, field);
        }

        private bool IsEqualsJoinObjects(object obj, Field field)
        {
            var aspect = DictionaryEntitiesAspects.GetInstance().GetAspect(objectUpdate);
            var valueA = GetObjectField(aspect, field);

            var valueB = GetObjectField(obj, field);

            if (valueA == null)
                return valueB == null;

            if (valueB == null)
                return false;

            Entity entityJoin = DictionaryEntitiesMap.INSTANCE.GetEntity(valueB.GetType());
            Field fieldId = entityJoin.FieldDictionary["id"];

            return IsEqualsField(valueB, fieldId);
        }

        private bool IsEqualsField(object obj, Field field)
        {
            var aspect = DictionaryEntitiesAspects.GetInstance().GetAspect(obj);

            if (aspect == null)
                return obj == null;

            var valueA = field.GetExtractor(obj).Extract();
            var valueB = field.GetExtractor(aspect).Extract();

            return IsEqualsObjects(valueA, valueB);
        }

        private static bool IsEqualsObjects(object valueA, object valueB)
        {
            return valueA == null ? valueB == null : valueA.Equals(valueB);
        }

        private string GetWhere()
        {
            Field idField = entity.FieldDictionary["id"];
            return String.Format("WHERE id = {0}", GetValueToUpdate(idField));
        }

        private string GetValueToUpdate(Field field)
        {
            if(!field.IsEntity)
                return new FieldValueFormatted(objectUpdate, field).Format();

            return GetJoinIdValue(field);
        }

        private string GetJoinIdValue(Field field)
        {
            var join = GetObjectField(objectUpdate, field);

            if (join == null)
                return "null";

            Entity entityJoin = DictionaryEntitiesMap.INSTANCE.GetEntity(join.GetType());
            Field idField = entityJoin.FieldDictionary["id"];

            return new FieldValueFormatted(join, idField).Format();
        }

        private object GetObjectField(object obj, Field field)
        {
            return field.GetExtractor(obj).Extract();
        }

    }
}

