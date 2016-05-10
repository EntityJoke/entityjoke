using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Core.Loaders
{
    public class EntitiesLoaderReflectionAdapter
    {
        private Dictionary<string, object> dictionary;
        private DataTable table;

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
