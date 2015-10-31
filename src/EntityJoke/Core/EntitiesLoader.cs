using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Core
{
    public class EntitiesLoader<T>
    {
        private DataTable produtoTable;
        private Entity entity;
        private List<T> listEntities = new List<T>();
        private Type type = typeof(T);
        private Dictionary<string, object> dictionaryObjectsProcessed;

        public EntitiesLoader(DataTable produtoTable)
        {
            this.produtoTable = produtoTable;
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(type);
        }

        public List<T> Load()
        {
            dictionaryObjectsProcessed = new Dictionary<string, object>();

            foreach (DataRow row in produtoTable.Rows)
                listEntities.Add(CreateInstance(row));

            return listEntities;
        }

        private T CreateInstance(DataRow row)
        {
            return (T)new EntityLoaderBuilder()
                .Entity(entity)
                .Row(row)
                .Columns(produtoTable.Columns)
                .Dictionary(dictionaryObjectsProcessed)
                .Build();
        }
    }
}
