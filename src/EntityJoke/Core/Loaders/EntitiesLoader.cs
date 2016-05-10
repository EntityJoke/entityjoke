using EntityJoke.Structure.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace EntityJoke.Core.Loaders
{
    public class EntitiesLoader<T>
    {
        private DataTable table;
        private Entity entity;
        private List<T> listEntities = new List<T>();
        private Type type = typeof(T);
        private Dictionary<string, object> dictionary;

        public EntitiesLoader(DataTable table)
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
            foreach (DataRow row in table.Rows)
                listEntities.Add(CreateInstance(row));

            return listEntities;
        }

        private T CreateInstance(DataRow row)
        {
            return (T)new EntityLoaderBuilder()
                .Entity(entity)
                .Row(row)
                .Columns(table.Columns)
                .Dictionary(GetDictionaryObjectsProcessed())
                .Build();
        }

        private Dictionary<string, object> GetDictionaryObjectsProcessed()
        {
            return dictionary != null ? dictionary : new Dictionary<string, object>();
        }
    }
}
