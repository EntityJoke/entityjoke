using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System;
using System.Collections.Generic;

namespace EntityJoke.Process.Generators
{
    public class EntityJoinsGenerator
    {
        private readonly Dictionary<string, Alias> dictionaryAliases = new Dictionary<string, Alias>();


        public EntityJoin Generate(Entity entity)
        {
            return Generate(entity, null);
        }

        private EntityJoin Generate(Entity entity, Field field)
        {
            var alias = CreateAlias(entity, field);
            var join = CreateEntityJoin(field, alias);

            entity.GetFieldsJoins()
                .ForEach(f => join.Joins.Add(Generate(f)));

            return join;
        }

        private static EntityJoin CreateEntityJoin(Field field, Alias alias)
        {
            return new EntityJoin
            {
                Alias = alias,
                Field = field
            };
        }

        private EntityJoin Generate(Field field)
        {
            return Generate(GetEntity(field.Type), field);
        }

        private static Entity GetEntity(Type type)
        {
            return DictionaryEntitiesMap.Get(type);
        }

        private Alias CreateAlias(Entity entity, Field field)
        {
            var alias = new Alias
            {
                Entity = entity,
                Symbol = CreateSymbol(entity)
            };

            dictionaryAliases.Add(alias.Symbol, alias);

            return alias;
        }

        private string CreateSymbol(Entity entity)
        {
            return new AliasGenerator(entity, dictionaryAliases).Generate();
        }

    }
}
