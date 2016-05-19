using EntityJoke.Structure.Entities;
using System.Collections.Generic;
using System.Data;

namespace EntityJoke.Core.Loaders
{
    public class EntityLoaderBuilder
    {
        private DataRow row;
        private PointerIndexColumn pointer = new PointerIndexColumn();
        private Entity entity;
        private DataColumnCollection columns;
        private Dictionary<string, object> dictionaryEntities = new Dictionary<string, object>();

        public EntityLoaderBuilder Entity(Entity entity)
        {
            this.entity = entity;
            return this;
        }

        public EntityLoaderBuilder Columns(DataColumnCollection columns)
        {
            this.columns = columns;
            return this;
        }

        public EntityLoaderBuilder Row(DataRow row)
        {
            this.row = row;
            return this;
        }

        public EntityLoaderBuilder PointerIndexColumn(PointerIndexColumn pointer)
        {
            this.pointer = pointer;
            return this;
        }

        public EntityLoaderBuilder Dictionary(Dictionary<string, object> dictionaryEntities)
        {
            this.dictionaryEntities = dictionaryEntities;
            return this;
        }

        public object Build()
        {
            var loader = new EntityLoader
            {
                Row = row,
                Pointer = pointer,
                Entity = entity,
                Columns = columns,
                DictionaryObjectsProcessed = dictionaryEntities
            };
            return loader.LoadInstance();
        }

    }
}
