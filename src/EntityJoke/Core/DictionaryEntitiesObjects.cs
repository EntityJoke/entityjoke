using EntityJoke.Process;
using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Core
{
    public class DictionaryEntitiesObjects
    {
        private static DictionaryEntitiesObjects instance = new DictionaryEntitiesObjects();

        private Dictionary<string, object> entityes = new Dictionary<string, object>();
        public int CountObjects { get { return entityes.Count; } }

        private DictionaryEntitiesObjects() { }

        public static DictionaryEntitiesObjects GetInstance()
        {
            return instance;
        }

        public void Clear()
        {
            entityes.Clear();
        }

        public void AddOrRefreshObject(object obj)
        {
            ProcessJoins(obj);

            if (IsObjectProcessed(obj))
                RefreshObject(obj);
            else
                AddObject(obj);
        }

        private void RefreshObject(object obj)
        {
            entityes[GetKey(obj)] = obj;
        }

        private void AddObject(object obj)
        {
            entityes.Add(GetKey(obj), obj);
        }

        private void ProcessJoins(object obj)
        {
            Entity ent = DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType());

            ent.GetFields().Where(f => f.IsEntity).ToList()
                .ForEach(f => ProcessJoins(obj, f));
        }

        private void ProcessJoins(object obj, Field f)
        {
            var objJoin = new ValueFieldExtractor(obj, f).Extract();
            DictionaryEntitiesObjects.GetInstance().AddOrRefreshObject(objJoin);
        }

        private bool IsObjectProcessed(object obj)
        {
            return entityes.ContainsKey(GetKey(obj));
        }

        private string GetKey(object obj)
        {
            return new KeyDictionaryObjectExtractor(obj).Extract(); 
        }

        private void CopyFieldValue(object origin, object copy, Field field)
        {
            new FieldValueSetter(copy, field, GetValue(origin, field)).Set();
        }

        private object GetValue(object origin, Field field)
        {
            return new ValueFieldExtractor(origin, field).Extract();
        }

        public object GetAspect(object obj)
        {
            if (IsObjectProcessed(obj))
                return entityes[GetKey(obj)];

            return null;
        }

        private void CopyObjects()
        {
            entityes.Keys.ToList()
                .ForEach(k => CopyObject(k));
        }

        private void CopyObject(string k)
        {
            entityes[k] = GetCopyObj(entityes[k]);
        }

        private object GetCopyObj(object obj)
        {
            object copy = Activator.CreateInstance(obj.GetType());

            DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType())
                .GetFields().ForEach(f => CopyFieldValue(obj, copy, f));

            return copy;
        }
    }
}
