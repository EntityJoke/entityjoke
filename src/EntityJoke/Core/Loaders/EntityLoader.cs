using EntityJoke.Process;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System;
using System.Collections;
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
        public Dictionary<string, object> Row;
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
            var columnName = CurrentColumnName();
            var value = Row[columnName];

            if (!IsNullValue(value))
                SetFieldValue(GetColumnField(), value);
        }

        private string CurrentColumnName()
        {
            return Row.Keys.ToList<string>()[Pointer.IndexColumn];
        }

        private static bool IsNullValue(object value)
        {
            return value.GetType() == typeof(DBNull);
        }

        private Field GetColumnField()
        {
            return Entity.FieldDictionary[GetOriginalName()];
        }

        private string GetOriginalName()
        {
            var currentColumnName = CurrentColumnName();
            var indexOf = currentColumnName.IndexOf("_");

            return currentColumnName.Substring(indexOf + 1);
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
