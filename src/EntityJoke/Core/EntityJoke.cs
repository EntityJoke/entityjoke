using EntityJoke.Linq;
using EntityJoke.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Core
{
    public class EntityJokes
    {

        public static void Save(object obj)
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(obj.GetType());
            new NonQueryCommandsExecutor(obj).Execute();
        }

        public static void Delete(object obj)
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(obj.GetType());
            new DeleteCommandsExecutor(obj).Execute();
        }

        public static QuerySimple<T> Query<T>()
        {
            return new QuerySimple<T>();
        }

    }
}
