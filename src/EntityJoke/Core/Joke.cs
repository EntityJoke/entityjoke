using EntityJoke.Linq;
using EntityJoke.Structure;
using System;
using System.Collections.Generic;

namespace EntityJoke.Core
{
    public class Joke<T>
    {
        private Entity entity;

        public Joke()
        {
            Type type = typeof(T);
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(type);
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(type);
        }

        public string GetEntityName()
        {
            return entity.Name;
        }

        public Dictionary<string, Field> GetEntityFields()
        {
            return entity.FieldDictionary;
        }

        public QuerySimple<T> Query()
        {
            return new QuerySimple<T>();
        }

        public List<T> GetAll()
        {
            return Query().Execute();
        }

    }
}
