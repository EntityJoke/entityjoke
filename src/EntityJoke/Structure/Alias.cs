using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Structure
{
    public class Alias
    {
        public Entity Entity;
        public string Symbol;
        public string EntityName { get { return Entity.Type.Name; } }

        public Alias(Entity entity, string symbol)
        {
            this.Entity = entity;
            this.Symbol = symbol;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", EntityName, Symbol);
        }
    }
}
