using EntityJoke.Structure.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace EntityJoke.Core.Loaders
{
    public class EntitiesLoader<T>
    {
        private readonly List<Dictionary<string, object>> table;
        private readonly Entity entity;
        private readonly List<T> listEntities = new List<T>();
        private readonly Type type = typeof(T);
        private Dictionary<string, object> dictionary;

        public EntitiesLoader(List<Dictionary<string, object>> table)
        {
            this.table = table;
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(type);
        }

        public void SetDictionaryObjectProcessed(Dictionary<string, object> dictionaryObjectsProcessed)
        {
            this.dictionary = dictionaryObjectsProcessed;
        }

        public List<T> Load()
        {
            foreach (var row in table)
                listEntities.Add(CreateInstance(row));

            return listEntities;
        }

        private T CreateInstance(Dictionary<string, object> row)
        {
            return (T)new EntityLoaderBuilder()
                .Entity(entity)
                .Row(row)
                .Dictionary(GetDictionaryObjectsProcessed())
                .Build();
        }

        private Dictionary<string, object> GetDictionaryObjectsProcessed()
        {
            return dictionary != null ? dictionary : new Dictionary<string, object>();
        }
    }
}
