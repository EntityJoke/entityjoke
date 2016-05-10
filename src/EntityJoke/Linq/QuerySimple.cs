using EntityJoke.Core;
using EntityJoke.Process.Commands;
using EntityJoke.Structure.Entities;
using System.Collections.Generic;

namespace EntityJoke.Linq
{
    public class QuerySimple<T>
    {
        private IQuerySimpleGenerator query;
        private Entity entity;

        public QuerySimple()
        {
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(typeof(T));
            query = QuerySimpleGeneratorFactory.Get();
            query.SetEntity(entity);
        }

        public QuerySimple<T> Where(string where)
        {
            query.SetWhere(where);
            return this;
        }

        public QuerySimple<T> OrderBy(string orderBy)
        {
            query.SetOrderBy(orderBy);
            return this;
        }

        public List<T> Execute()
        {
            return SQLCommandExecutorFactory<T>.Get(query.GetSqlCommand()).Execute();
        }

        public override string ToString()
        {
            return query.GetSqlCommand();
        }
    }
}
