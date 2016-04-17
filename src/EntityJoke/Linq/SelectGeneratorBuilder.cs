using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityJoke.Linq
{
    public class SelectGeneratorBuilder : SQLGeneratorBuilder<SelectGeneratorBuilder>
    {
        private const string SEPARATOR = ", ";

        private string select;

        public override string Build()
        {
            AddEntityAlias(entity.TreeJoins);
            return select.Substring(SEPARATOR.Length);
        }

        private void AddEntityAlias(EntityJoin entityAliasTree)
        {
            select += GetSelectFields(entityAliasTree);
            entityAliasTree.Joins.ForEach(j => AddEntityAlias(j));
        }

        private string GetSelectFields(EntityJoin entityAliasTree)
        {
            string selectFields = "";

            GetFieldsOrdered(entityAliasTree)
                .ForEach(f => selectFields += GetColumn(entityAliasTree, f));

            return selectFields;
        }

        private static List<Field> GetFieldsOrdered(EntityJoin entityAliasTree)
        {
            return entityAliasTree.Entity.GetFields()
                .Where(f => !f.IsEntity)
                .OrderBy(f => !f.IsKey)
                .ThenBy(f => f.Name).ToList();
        }

        private string GetColumn(EntityJoin join, Field field)
        {
            return String.Format("{0}{1} {2}", 
                SEPARATOR, 
                GetColumnName(join, field),
                GetColumnAlias(join, field));
        }

        private string GetColumnName(EntityJoin join, Field field)
        {
            return String.Format("{0}.{1}", join.Alias.Symbol, field.ColumnName);
        }

        private string GetColumnAlias(EntityJoin join, Field field)
        {
            return String.Format("{0}_{1}", join.Alias.Symbol, field.ColumnName);
        }

    }
}
