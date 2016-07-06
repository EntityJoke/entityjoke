using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;

namespace EntityJoke.Linq
{
    public class FieldAliasReplacerBuilder
    {
        private EntityJoin join;
        private Field field;
        private string sqlQuery;
        private string entityPath;

        public FieldAliasReplacerBuilder Entity(EntityJoin join)
        {
            this.join = join;
            return this;
        }

        public FieldAliasReplacerBuilder Field(Field field)
        {
            this.field = field;
            return this;
        }

        public FieldAliasReplacerBuilder SqlQuery(string sqlQuery)
        {
            this.sqlQuery = sqlQuery;
            return this;
        }

        public FieldAliasReplacerBuilder EntityPath(string entityPath)
        {
            this.entityPath = entityPath;
            return this;
        }

        public FieldAliasReplacer Builder()
        {
            var replacer = FieldAliasReplacerFactory.Get(field);
            replacer.Entity = join;
            replacer.Field = field;
            replacer.SQLQuery = sqlQuery;
            replacer.EntityPath = entityPath;

            return replacer;
        }
    }
}
