using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityJoke.Process.Commands
{
    public class InsertCommandGenerator : ICommandSQLGenerator
    {
        object objectInsert;
        Entity entity;

        public InsertCommandGenerator(object objectInsert)
        {
            this.objectInsert = objectInsert;
            this.entity = DictionaryEntitiesMap.INSTANCE.GetEntity(objectInsert.GetType());
        }

        public string Generate()
        {
            return GetInsertThisObject();
        }

        private string GetInsertThisObject()
        {
            return String.Format("{0} {1} VALUES {2} RETURNING ID",
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
                columns += GetColumnName(field);

            return String.Format("({0})", columns.Substring(2));
        }

        private string GetColumnName(Field field)
        {
            var columnName = String.Format(", {0}", field.ColumnName);

            if(!field.IsEntity)
                return columnName;

            return GetJoinIdValue(field) == null ? "" : columnName;
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
                values += AddValueInsert(field);

            return String.Format("({0})", values.Substring(2));
        }

        private string AddValueInsert(Field field)
        {
            var value = GetValueToInsert(field);

            return value == null ? "" : String.Format(", {0}", value);
        }

        private string GetValueToInsert(Field field)
        {
            if(!field.IsEntity)
                return new FieldValueFormatted(objectInsert, field).Format();

            return GetJoinIdValue(field);
        }

        private string GetJoinIdValue(Field field)
        {
            object join = field.GetExtractor(objectInsert).Extract();

            if (join == null)
                return null;

            Entity entityJoin = DictionaryEntitiesMap.INSTANCE.GetEntity(join.GetType());
            Field idField = entityJoin.FieldDictionary["id"];

            return new FieldValueFormatted(join, idField).Format();
        }

    }
}

