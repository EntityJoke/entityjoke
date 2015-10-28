using EntityJoke.Core;
using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Process
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
