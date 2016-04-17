using EntityJoke.Process;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System.Collections.Generic;
using System.Linq;

namespace EntityJoke.Core
{
    public class DictionaryEntitiesAspects
    {
        private static DictionaryEntitiesAspects instance = new DictionaryEntitiesAspects();

        private Dictionary<string, object> entityes = new Dictionary<string, object>();
        public int CountObjects { get { return entityes.Count; } }

        private DictionaryEntitiesAspects() { }

        public static DictionaryEntitiesAspects GetInstance()
        {
            return instance;
        }

        public void Clear()
        {
            entityes.Clear();
        }

        public void AddOrRefreshAspect(object obj)
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
            entityes[GetKey(obj)] = new ClonerObject(obj).clone();
        }

        private void AddObject(object obj)
        {
            entityes.Add(GetKey(obj), new ClonerObject(obj).clone());
        }

        private void ProcessJoins(object obj)
        {
            if (obj == null)
                return;

            Entity ent = DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType());

            ent.GetFields().Where(f => f.IsEntity).ToList()
                .ForEach(f => ProcessJoins(obj, f));
        }

        private void ProcessJoins(object obj, Field f)
        {
            var objJoin = f.GetExtractor(obj).Extract();
            DictionaryEntitiesAspects.GetInstance().AddOrRefreshAspect(objJoin);
        }

        private bool IsObjectProcessed(object obj)
        {
            return entityes.ContainsKey(GetKey(obj));
        }

        private string GetKey(object obj)
        {
            return new KeyDictionaryObjectExtractor(obj).Extract(); 
        }

        public object GetAspect(object obj)
        {
            if (IsObjectProcessed(obj))
                return entityes[GetKey(obj)];

            return null;
        }
    }
}
