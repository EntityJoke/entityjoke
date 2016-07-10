using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EntityJoke.Structure.Fields
{
    internal class FieldFactory
    {

        internal static Field Get(MemberInfo info)
        {
            var creator = new FieldInfoCreator(info);

            if(!IsSystemClass(creator.Type))
                return CreateFieldEntity(creator);

            if(IsEntityEnumerable(creator.Type))
                return CreateFieldEnumerableEntity(creator);

            return CreateField(creator);
        }

        private static bool IsSystemClass(Type type)
        {
            return type.FullName.Contains("System.");
        }

        private static FieldEntity CreateFieldEntity(FieldInfoCreator creator)
        {
            return new FieldEntity(creator);
        }

        private static bool IsEntityEnumerable(Type type)
        {
            List<Type> interfaces = new List<Type>(type.GetInterfaces());
            return interfaces.Any(t => t.Name == "ICollection");
        }

        private static Field CreateFieldEnumerableEntity(FieldInfoCreator creator)
        {
            return new FieldCollectionEntity(creator);
        }

        private static Field CreateField(FieldInfoCreator creator)
        {
            return new Field(creator);
        }
    }
}
