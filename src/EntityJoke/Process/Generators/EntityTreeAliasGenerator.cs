using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityJoke.Process.Generators
{
    public class EntityJoinsGenerator
    {
        private Dictionary<string, Alias> dictionaryAliases = new Dictionary<string, Alias>();


        public EntityJoin Generate(Entity entity)
        {
            return Generate(entity, null);
        }

        private EntityJoin Generate(Entity entity, Field field)
        {
            Alias alias = CreateAlias(entity, field);
            EntityJoin join = new EntityJoin(alias);

            entity.GetFields().Where(f => f.IsEntity).ToList()
                .ForEach(f => join.Joins.Add(Generate(f)));

            return join;
        }

        private EntityJoin Generate(Field field)
        {
            EntityJoin join = Generate(GetEntity(field.Type), field);
            join.Field = field;
            return join;
        }

        private Entity GetEntity(Type type)
        {
            return DictionaryEntitiesMap.INSTANCE.GetEntity(type);
        }

        private Alias CreateAlias(Entity entity, Field field)
        {
            Alias alias = new Alias(entity, CreateSymbol(entity));
            dictionaryAliases.Add(alias.Symbol, alias);
            return alias;
        }

        private string CreateSymbol(Entity entity)
        {
            return new AliasGenerator(entity, dictionaryAliases).Generate();
        }

    }
}
