using System.Collections.Generic;
using System.Data;

namespace EntityJoke.Core.Loaders
{
    public class EntitiesLoaderReflectionAdapter
    {
        private readonly Dictionary<string, object> dictionary;
        private readonly DataTable table;

        public EntitiesLoaderReflectionAdapter(DataTable table, Dictionary<string, object> dictionary)
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
