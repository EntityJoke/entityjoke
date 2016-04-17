using EntityJoke.Structure.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace EntityJoke.Core
{
    public class EntitiesLoader<T>
    {
        private DataTable table;
        private Entity entity;
        private List<T> listEntities = new List<T>();
        private Type type = typeof(T);
        private Dictionary<string, object> dictionaryObjectsProcessed;

        public EntitiesLoader(DataTable table)
        {
            this.table = table;
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(type);
        }

        public List<T> Load()
        {
            dictionaryObjectsProcessed = new Dictionary<string, object>();

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
                .Dictionary(dictionaryObjectsProcessed)
                .Build();
        }
    }
}
