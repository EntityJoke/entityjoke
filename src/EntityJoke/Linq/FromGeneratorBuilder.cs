using EntityJoke.Structure.Entities;
using System;

namespace EntityJoke.Linq
{
    public class FromGeneratorBuilder: SQLGeneratorBuilder<FromGeneratorBuilder>
    {
        private const string FORMAT_JOIN = " LEFT JOIN {0} {1} ON ({1}.{2} = {3}.{4})";

        string from;

        public override string Build()
        {
            from = "";

            AddEntityMain(entity.TreeJoins);
            ProcessJoins(entity.TreeJoins);

            return from;
        }

        private void AddEntityMain(EntityJoin entity)
        {
            from += $"{entity.Entity.Name} {entity.Alias.Symbol}";
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
