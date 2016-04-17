using EntityJoke.Process.Generators;
using System;
using System.Reflection;

namespace EntityJoke.Structure.Fields
{
    internal class FieldFactory
    {

        internal static Field Get(MemberInfo info)
        {
            FieldInfoCreator creator = GetCreator(info);

            if(IsSystemClass(creator.Type))
                return CreateField(creator);

            return CreateField(creator);
        }

        private static FieldInfoCreator GetCreator(MemberInfo info)
        {
            var creator = new FieldInfoCreator(info);
            return creator;
        }

        private static Field CreateField(FieldInfoCreator creator)
        {
            return new Field(creator);
        }

        private static bool IsSystemClass(Type type)
        {
            return type.FullName.Contains("System.");
        }
    }
}
