using System.Collections.Generic;

namespace EntityJoke.Core.Loaders
{
    public class EntitiesLoaderReflectionAdapter
    {
        private readonly Dictionary<string, object> dictionary;
        private readonly List<Dictionary<string, object>> table;

        public EntitiesLoaderReflectionAdapter(List<Dictionary<string, object>> table, Dictionary<string, object> dictionary)
        {
            this.table = table;
            this.dictionary = dictionary;
        }

        public object Load<T>()
        {
            var loader = new EntitiesLoader<T>(table);
            loader.SetDictionaryObjectProcessed(dictionary);
            return loader.Load();
        }


    }
}
