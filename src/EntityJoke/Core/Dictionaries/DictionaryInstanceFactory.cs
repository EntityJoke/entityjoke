using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Core
{
    public class DictionaryInstanceFactory
    {

        private static DictionaryInstanceFactory instance = new DictionaryInstanceFactory();

        private Dictionary<string, object> values = new Dictionary<string, object>();

        private DictionaryInstanceFactory() { }

        public void Set(string key, object value)
        {
            if(values.ContainsKey(key))
                values[key] = value;
            else
                values.Add(key, value);
        }

        public static bool IsDataTableGeneratorMock()
        {
            return instance.values.ContainsKey("DataTableGeneratorMock") ? (bool)instance.values["DataTableGeneratorMock"] : false;
        }

        public static void AddDataTableMock(DataTable dataTable)
        {
            Queue<DataTable> q = new Queue<DataTable>();

            if (instance.values.ContainsKey("DataTableMockQueue"))
                q = GetDataTableQueue();
            else
                instance.values.Add("DataTableMockQueue", q);

            q.Enqueue(dataTable);
        }

        private static Queue<DataTable> GetDataTableQueue()
        {
            return (Queue<DataTable>)instance.values["DataTableMockQueue"];
        }

        internal static DataTable GetDataTable()
        {
            return GetDataTableQueue().Dequeue();
        }

        public static DictionaryInstanceFactory GetInstance()
        {
            return instance;
        }

    }
}
