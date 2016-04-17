using System;

namespace EntityJoke.Structure.Entities
{
    internal class EntityFactory
    {

        internal static Entity Get(Type type)
        {
            return new Entity(type);
        }

    }
}
