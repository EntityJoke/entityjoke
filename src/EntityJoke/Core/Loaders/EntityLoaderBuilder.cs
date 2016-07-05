using EntityJoke.Structure.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace EntityJoke.Core.Loaders
{
    public class EntityLoaderBuilder
    {
        private Dictionary<string, object> row;
        private PointerIndexColumn pointer = new PointerIndexColumn();
        private Entity entity;
        private Dictionary<string, object> dictionaryEntities = new Dictionary<string, object>();

        public EntityLoaderBuilder Entity(Entity entity)
        {
            this.entity = entity;
            return this;
        }

        public EntityLoaderBuilder Row(Dictionary<string, object> row)
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
                DictionaryObjectsProcessed = dictionaryEntities
            };
            return loader.LoadInstance();
        }

    }
}
