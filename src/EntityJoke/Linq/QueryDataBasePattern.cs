using EntityJoke.Structure.Entities;
using System;

namespace EntityJoke.Linq
{
    public class QueryDataBasePattern : IQuerySimpleGenerator
    {
        private string where;
        private string orderBy;
        private Entity entity;

        public void SetEntity(Entity entity)
        {
            this.entity = entity;
        }

        public void SetWhere(string where)
        {
            this.where = where;
        }

        public void SetOrderBy(string orderBy)
        {
            this.orderBy = orderBy;
        }

        public string GetSqlCommand()
        {
            var sqlCommand = $"SELECT {GetSelect()} FROM {GetFrom()}{GetWhere()}{GetOrderBy()}".Trim();
            return new SQLCommandAliasReplacer(entity, sqlCommand).Replace();
        }

        private string GetSelect()
        {
            return new SelectGeneratorBuilder()
                .SetEntity(entity)
                .Build();
        }

        private string GetFrom()
        {
            return new FromGeneratorBuilder()
                .SetEntity(entity)
                .Build();
        }

        private string GetWhere()
        {
            return new WhereGeneratorBuilder()
                .SetCondition(where)
                .Build();
        }

        private string GetOrderBy()
        {
            return new OrderByGeneratorBuilder()
                .SetCondition(orderBy)
                .Build();
        }

    }
}
