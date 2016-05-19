using EntityJoke.Process;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EntityJoke.Core.Loaders
{
    public class EntityLoader
    {
        private object obj;

        public PointerIndexColumn Pointer;
        public Entity Entity;
        public DataRow Row;
        public DataColumnCollection Columns;

        public Dictionary<string, object> DictionaryObjectsProcessed;

        public object LoadInstance()
        {
            obj = Activator.CreateInstance(Entity.Type);
            LoadObject();
            return obj;
        }

        private void LoadObject()
        {
            if (IsObjectProcessed())
                RecoverObject();
            else
                CreateObject();
        }

        private bool IsObjectProcessed()
        {
            ProcessField();
            return DictionaryObjectsProcessed.ContainsKey(GetKey(obj));
        }

        private static string GetKey(object obj)
        {
            return new KeyDictionaryObjectExtractor(obj).Extract();
        }

        private object GetObjectInDictionary()
        {
            return DictionaryEntitiesObjects.GetInstance().GetObject(obj);
        }

        private void RecoverObject()
        {
            obj = GetObjectInDictionary();
            var fieldsNotEntity = Entity.GetFields().Where(f => !f.IsEntity).ToList().Count;
            Pointer.IndexColumn += fieldsNotEntity;
        }

        private void CreateObject()
        {
            PutObjectInDictionary();

            ProccesColumns();
            ProcessJoins();
            ProcessCollections();

            AddOrRefreshAspect();
        }

        private void ProccesColumns()
        {
            var limiteLoop = EntityColumnsLength();

            for (; Pointer.IndexColumn < limiteLoop; Pointer.IndexColumn++)
                ProcessField();
        }

        private int EntityColumnsLength()
        {
            var fieldsNotEntity = Entity.GetFields().Where(f => !f.IsEntity).ToList().Count;
            return fieldsNotEntity + Pointer.IndexColumn;
        }

        private void ProcessField()
        {
            var column = GetCurrentColumn();
            var value = Row[column.ColumnName];

            if (!IsNullValue(value))
                SetFieldValue(GetColumnField(column), value);
        }

        private DataColumn GetCurrentColumn()
        {
            return Columns[Pointer.IndexColumn];
        }

        private static bool IsNullValue(object value)
        {
            return value.GetType() == typeof(DBNull);
        }

        private Field GetColumnField(DataColumn column)
        {
            return Entity.FieldDictionary[GetOriginalName(column)];
        }

        private static string GetOriginalName(DataColumn column)
        {
            var indexOf = column.ColumnName.IndexOf("_");
            return column.ColumnName.Substring(indexOf + 1);
        }

        private void SetFieldValue(Field field, object value)
        {
            field.GetSetter(obj, value).Set();
        }

        private void ProcessJoins()
        {
            Entity.Joins.ForEach(j => ProcessJoin(j));
        }

        private void ProcessJoin(EntityJoin join)
        {
            SetFieldValue(join.Field, GetJoinValue(join));
        }

        private object GetJoinValue(EntityJoin join)
        {
            return new EntityLoaderBuilder()
                .Entity(join.Entity)
                .Row(Row)
                .Columns(Columns)
                .PointerIndexColumn(Pointer)
                .Dictionary(DictionaryObjectsProcessed)
                .Build();
        }

        private void PutObjectInDictionary()
        {
            if (IsObjectInDictionaryEntities())
                RefreshObject();
            else
                DictionaryEntitiesObjects.GetInstance().AddOrRefreshObject(obj);

            DictionaryObjectsProcessed.Add(GetKey(obj), obj);
        }

        private bool IsObjectInDictionaryEntities()
        {
            return DictionaryEntitiesObjects.GetInstance().GetObject(obj) != null;
        }

        private void RefreshObject()
        {
            var objectDictionary = DictionaryEntitiesObjects.GetInstance().GetObject(obj);
            Entity.GetFields().ForEach(f => RefreshFieldValue(objectDictionary, f));
            obj = objectDictionary;
        }

        private void RefreshFieldValue(object objectDictionary, Field field)
        {
            field.GetSetter(objectDictionary, GetValue(field)).Set();
        }

        private object GetValue(Field field)
        {
            return field.GetExtractor(obj).Extract();
        }

        private void ProcessCollections()
        {
            new CollectionsLoader(obj, DictionaryObjectsProcessed).Load();
        }

        private void AddOrRefreshAspect()
        {
            DictionaryEntitiesAspects.GetInstance().AddOrRefreshAspect(obj);
        }
    }
}
