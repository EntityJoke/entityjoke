using EntityJoke.Structure.Fields;
using System.Collections.Generic;

namespace EntityJoke.Structure.Entities
{
    public class EntityJoin
    {
        public Alias Alias;
        public Field Field;
        public string TableName { get { return Entity.Name; } }
        public string Name { get { return Entity.Type.Name; } }
        public readonly List<EntityJoin> Joins = new List<EntityJoin>();

        public Entity Entity { get { return Alias.Entity; } }

        public override string ToString()
        {
            return $"{Entity.Name} - {Field}";
        }

    }
}
