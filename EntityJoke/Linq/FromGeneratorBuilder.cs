using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityJoke.Linq
{
    public class FromGeneratorBuilder: SQLGeneratorBuilder<FromGeneratorBuilder>
    {
        private const string FORMAT_JOIN = " JOIN {0} {1} ON ({1}.{2} = {3}.{4})";

        string from;

        public override string Build()
        {
            from = "";

            AddEntityMain(entity.TreeAliases);
            ProcessJoins(entity.TreeAliases);

            return from;
        }

        private void AddEntityMain(EntityJoin entity)
        {
            from += String.Format("{0} {1}", entity.Entity.Name, entity.Alias.Symbol);
        }

        private void ProcessJoins(EntityJoin entityA)
        {
            entityA.Joins.ForEach(entityB => ProcessJoin(entityA, entityB));
        }

        private void ProcessJoin(EntityJoin entityA, EntityJoin entityB)
        {
            AddJoin(entityA, entityB);
            ProcessJoins(entityB);
        }

        private void AddJoin(EntityJoin entityA, EntityJoin entityB)
        {
            from += String.Format(FORMAT_JOIN,
                GetTableName(entityB),
                GetAlias(entityB),
                GetPrimaryKey(),
                GetAlias(entityA),
                GetForeignKey(entityB));
        }

        private static string GetTableName(EntityJoin entity)
        {
            return entity.TableName;
        }

        private static string GetPrimaryKey()
        {
            return "id";
        }

        private static string GetAlias(EntityJoin entity)
        {
            return entity.Alias.Symbol;
        }

        private static string GetForeignKey(EntityJoin entity)
        {
            return entity.Field.ColumnName;
        }

    }
}
