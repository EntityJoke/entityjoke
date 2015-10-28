using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Structure
{
    public class EntityJoin
    {
        public Alias Alias;
        public Field Field;
        public List<EntityJoin> Joins = new List<EntityJoin>();
        public string TableName { get { return Entity.Name; } }
        public string Name { get { return Entity.Type.Name; } }

        public Entity Entity { get { return Alias.Entity; } }

        public EntityJoin(Alias alias)
        {
            this.Alias = alias;
        }

        public override string ToString()
        {
            return Entity.Name + " - " + Field;
        }

    }
}
