using System.Collections.Generic;

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

        public static void AddDataTableMock(List<Dictionary<string, object>> dataTable)
        {
            var q = new Queue<List<Dictionary<string, object>>>();

            if (instance.values.ContainsKey("DataTableMockQueue"))
                q = GetDataTableQueue();
            else
                instance.values.Add("DataTableMockQueue", q);

            q.Enqueue(dataTable);
        }

        private static Queue<List<Dictionary<string, object>>> GetDataTableQueue()
        {
            return (Queue<List<Dictionary<string, object>>>)instance.values["DataTableMockQueue"];
        }

        internal static List<Dictionary<string, object>> GetDataTable()
        {
            return GetDataTableQueue().Dequeue();
        }

        public static DictionaryInstanceFactory GetInstance()
        {
            return instance;
        }

    }
}
