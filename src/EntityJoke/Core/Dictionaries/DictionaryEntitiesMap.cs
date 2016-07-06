using EntityJoke.Structure.Entities;
using System;
using System.Collections.Generic;

namespace EntityJoke.Core
{
    public class DictionaryEntitiesMap
    {

        public static DictionaryEntitiesMap INSTANCE { get; set; } = new DictionaryEntitiesMap();

        private readonly Dictionary<string, Entity> entityes = new Dictionary<string, Entity>();

        private DictionaryEntitiesMap() { }

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

        private static Entity LoadEntity(Type type)
        {
            return EntityFactory.Get(type);
        }

        public Entity GetEntity(Type type)
        {
            if (!ExistsEntity(type))
                entityes.Add(type.FullName, LoadEntity(type));

            return entityes[type.FullName];
        }

        public int EntityesCount()
        {
            return entityes.Count;
        }

        public void AddEntity(Type type)
        {
            TryAddEntity(type);
        }

        public static Entity Get(Type type)
        {
            return Instance().GetEntity(type);
        }

        public static void Clear()
        {
            Instance().entityes.Clear();
        }

        public static DictionaryEntitiesMap Instance()
        {
            return INSTANCE;
        }
    }
}
