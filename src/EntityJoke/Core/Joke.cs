using EntityJoke.Linq;
using EntityJoke.Process.Commands;
using System.Collections.Generic;

namespace EntityJoke.Core
{
    public class Joke
    {
        public static QuerySimple<T> Query<T>()
        {
            return new QuerySimple<T>();
        }

        public static List<T> GetAll<T>()
        {
            return Query<T>().Execute();
        }

        public static T GetById<T>(int id)
        {
            var entity = DictionaryEntitiesMap.INSTANCE.GetEntity(typeof(T));
            return Query<T>()
                .Where(entity.TreeJoins.Alias.Symbol + ".id = " + id)
                .Execute()[0];
        }

        public static void Save(object obj)
        {
            new NonQueryCommandsExecutor(obj).Execute();
        }

        public static void Delete(object obj)
        {
            new DeleteCommandsExecutor(obj).Execute();
        }
    }
}