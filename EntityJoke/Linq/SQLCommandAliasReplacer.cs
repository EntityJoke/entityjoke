using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Linq
{
    public class SQLCommandAliasReplacer
    {
        private string sqlCommand;
        private Entity entity;

        public SQLCommandAliasReplacer(Entity entity, string sqlCommand)
        {
            this.entity = entity;
            this.sqlCommand = sqlCommand;
        }

        public string Replace()
        {
            ReplaceFieldsEntity(entity.TreeAliases);

            return sqlCommand;
        }

        private void ReplaceFieldsEntity(EntityJoin join, string entityPath = "")
        {
            ProcessAlias(join, entityPath);
            join.Joins.ForEach(j => ReplaceFieldsEntity(j, GetCompletedNameField(join, entityPath)));
        }

        private void ProcessAlias(EntityJoin join, string entityPath)
        {
            join.Entity.GetFields()
                .OrderBy(f => f.ColumnName.Length)
                .Reverse()
                .Where(f => !f.IsEntity).ToList()
                .ForEach(field => ReplaceConditionField(join, field, entityPath));
        }

        private string GetCompletedNameField(EntityJoin join, string entityPath)
        {
            return IsEntityMain(join) ? GetEntityName(join) : GetNameFieldWithEntityPath(join, entityPath);
        }

        private static string GetEntityName(EntityJoin join)
        {
            return join.Entity.Type.Name;
        }

        private string GetNameFieldWithEntityPath(EntityJoin join, string entityPath)
        {
            return String.Format("{0}.{1}",entityPath, join.Field.Name);
        }

        private bool IsEntityMain(EntityJoin join)
        {
            return join.Field == null;
        }

        private void ReplaceConditionField(EntityJoin join, Field field, string entityPath)
        {
            FieldAliasReplacer replacer = new FieldAliasReplacerBuilder()
                .Join(join)
                .Field(field)
                .SqlQuery(sqlCommand)
                .EntityPath(entityPath)
                .Builder();

            sqlCommand = replacer.Replace();
        }
    }
}
