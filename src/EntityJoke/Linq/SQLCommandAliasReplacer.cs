using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System;
using System.Linq;

namespace EntityJoke.Linq
{
    public class SQLCommandAliasReplacer
    {
        private string sqlCommand;
        private Entity entityMain;

        public SQLCommandAliasReplacer(Entity entity, string sqlCommand)
        {
            this.entityMain = entity;
            this.sqlCommand = sqlCommand;
        }

        public string Replace()
        {
            ReplaceFieldsEntity(entityMain.TreeJoins);

            return sqlCommand;
        }

        private void ReplaceFieldsEntity(EntityJoin entity, string entityPath = "")
        {
            ProcessAlias(entity, entityPath);
            entity.Joins.ForEach(j => ReplaceFieldsEntity(j, GetCompletedNameField(entity, entityPath)));
        }

        private void ProcessAlias(EntityJoin entity, string entityPath)
        {
            entity.Entity.GetFields()
                .OrderBy(f => f.ColumnName.Length)
                .Reverse()
                .Where(f => !f.IsEntity).ToList()
                .ForEach(field => ReplaceConditionField(entity, field, entityPath));
        }

        private string GetCompletedNameField(EntityJoin entity, string entityPath)
        {
            return IsEntityMain(entity) ? entity.Name : GetNameFieldWithEntityPath(entity, entityPath);
        }

        private bool IsEntityMain(EntityJoin entity)
        {
            return entityMain == entity.Entity;
        }

        private string GetNameFieldWithEntityPath(EntityJoin join, string entityPath)
        {
            return String.Format("{0}.{1}",entityPath, join.Field.Name);
        }

        private void ReplaceConditionField(EntityJoin join, Field field, string entityPath)
        {
            FieldAliasReplacer replacer = new FieldAliasReplacerBuilder()
                .Entity(join)
                .Field(field)
                .SqlQuery(sqlCommand)
                .EntityPath(entityPath)
                .Builder();

            sqlCommand = replacer.Replace();
        }
    }
}
