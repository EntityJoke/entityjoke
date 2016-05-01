using EntityJoke.Process.Generators;
using System;
using System.Reflection;

namespace EntityJoke.Structure.Fields
{
    public class FieldInfoCreator
    {
        public string ColumnName;
        public string Name;
        public bool IsProperty;
        public Type Type;
        public string DeclaringTypeName;

        public FieldInfoCreator(MemberInfo info)
        {
            this.ColumnName = new NameFieldGenerator(info).Generate();
            this.Name = info.Name.Replace("get_", "");
            this.IsProperty = GetIsProperty(info);
            this.Type = GetType(info);
            this.DeclaringTypeName = info.DeclaringType.FullName;
        }

        private bool GetIsProperty(MemberInfo info)
        {
            return info is MethodInfo;
        }

        private Type GetType(MemberInfo info)
        {
            return IsProperty ? TypeByMethod(info) : TypeByField(info);
        }

        private Type TypeByMethod(MemberInfo info)
        {
            return ((MethodInfo)info).ReturnType;
        }

        private Type TypeByField(MemberInfo info)
        {
            return ((FieldInfo)info).FieldType;
        }
    }
}