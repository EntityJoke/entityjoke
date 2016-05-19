using System.Collections.Generic;
using System.Data;

namespace EntityJoke.Core
{
    public class DictionaryInstanceFactory
    {
        private static readonly DictionaryInstanceFactory instance = new DictionaryInstanceFactory();

        private readonly Dictionary<string, object> values = new Dictionary<string, object>();

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

        public static void SetDataTableGeneratorMock(bool isMock)
        {
            instance.Set("DataTableGeneratorMock", isMock);
        }

        public static void AddDataTableMock(DataTable dataTable)
        {
            var q = new Queue<DataTable>();

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
