using EntityJoke.Process;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System.Collections.Generic;
using System.Linq;

namespace EntityJoke.Core
{
    public class DictionaryEntitiesObjects
    {
        private static readonly DictionaryEntitiesObjects instance = new DictionaryEntitiesObjects();
        private readonly Dictionary<string, object> entityes = new Dictionary<string, object>();

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
            if (obj == null)
                return;

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

        private static void ProcessJoins(object obj)
        {
            var ent = DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType());

            ent.GetFieldsJoins()
                .ForEach(f => ProcessJoins(obj, f));
        }

        private static void ProcessJoins(object obj, Field f)
        {
            var objJoin = f.GetExtractor(obj).Extract();
            DictionaryEntitiesObjects.GetInstance().AddOrRefreshObject(objJoin);
        }

        private bool IsObjectProcessed(object obj)
        {
            return entityes.ContainsKey(GetKey(obj));
        }

        private static string GetKey(object obj)
        {
            return new KeyDictionaryObjectExtractor(obj).Extract();
        }

        public object GetObject(object obj)
        {
            if (IsObjectProcessed(obj))
                return entityes[GetKey(obj)];

            return null;
        }
    }
}
