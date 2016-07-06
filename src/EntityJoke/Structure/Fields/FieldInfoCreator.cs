using EntityJoke.Process.Generators;
using System;
using System.Reflection;

namespace EntityJoke.Structure.Fields
{
    public class FieldInfoCreator
    {
        public readonly string ColumnName;
        public readonly string Name;
        public readonly bool IsProperty;
        public readonly Type Type;

        public FieldInfoCreator(MemberInfo info)
        {
            ColumnName = new NameFieldGenerator(info).Generate();
            Name = info.Name.Replace("get_", "");
            IsProperty = GetIsProperty(info);
            Type = GetType(info);
        }

        private static bool GetIsProperty(MemberInfo info)
        {
            return info is MethodInfo;
        }

        private Type GetType(MemberInfo info)
        {
            return IsProperty ? TypeByMethod(info) : TypeByField(info);
        }

        private static Type TypeByMethod(MemberInfo info)
        {
            return ((MethodInfo)info).ReturnType;
        }

        private static Type TypeByField(MemberInfo info)
        {
            return ((FieldInfo)info).FieldType;
        }
    }
}