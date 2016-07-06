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
        readonly Entity entity;
        readonly object objectUpdate;

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
            return $"{GetUpdate()} {GetColumns()} {GetWhere()} RETURNING ID";
        }

        private string GetUpdate()
        {
            return $"UPDATE {entity.Name}";
        }

        private string GetColumns()
        {
            var builder = new System.Text.StringBuilder();

            foreach (Field field in GetFieldsToUpdateOrdered())
                builder.Append($", {field.ColumnName} = {GetValueToUpdate(field)}");

            var columns = builder.ToString();

            return $"SET {(columns.Length > 0 ? columns.Substring(2) : "")}";
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

            var entityJoin = DictionaryEntitiesMap.INSTANCE.GetEntity(valueB.GetType());
            var fieldId = entityJoin.FieldDictionary["id"];

            return IsEqualsField(valueB, fieldId);
        }

        private static bool IsEqualsField(object obj, Field field)
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
            var idField = entity.FieldDictionary["id"];
            return $"WHERE id = {GetValueToUpdate(idField)}";
        }

        private string GetValueToUpdate(Field field)
        {
            if(!field.IsEntity)
                return new FieldValueFormater(objectUpdate, field).Format();

            return GetJoinIdValue(field);
        }

        private string GetJoinIdValue(Field field)
        {
            var join = GetObjectField(objectUpdate, field);

            if (join == null)
                return "null";

            var entityJoin = DictionaryEntitiesMap.INSTANCE.GetEntity(join.GetType());
            var idField = entityJoin.FieldDictionary["id"];

            return new FieldValueFormater(join, idField).Format();
        }

        private static object GetObjectField(object obj, Field field)
        {
            return field.GetExtractor(obj).Extract();
        }

    }
}

