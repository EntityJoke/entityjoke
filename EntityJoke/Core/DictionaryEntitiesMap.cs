using EntityJoke.Structure;
using System;
using System.Collections.Generic;

namespace EntityJoke.Core
{
    public class DictionaryEntitiesMap
    {
        public static DictionaryEntitiesMap INSTANCE = new DictionaryEntitiesMap();

        private Dictionary<string, Entity> entityes = new Dictionary<string,Entity>();

        private DictionaryEntitiesMap() { }


        public void Clear()
        {
            entityes.Clear();
        }

        public bool TryAddEntity(Type type)
        {
            if (ExistsEntity(type))
                return false;

            entityes.Add(type.FullName, LoadEntity(type));

            return true;
        }

        public bool ExistsEntity(Type type)
        {
            return entityes.ContainsKey(type.FullName);
        }

        private Entity LoadEntity(Type type)
        {
            return new Entity(type);
        }

        public Entity GetEntity(Type type)
        {
            Entity entity;
            return entityes.TryGetValue(type.FullName, out entity) ? entity : null;
        }

        public int EntityesCount()
        {
            return entityes.Count;
        }

        public void AddEntity(Type type)
        {
            TryAddEntity(type);
        }
    }
}
