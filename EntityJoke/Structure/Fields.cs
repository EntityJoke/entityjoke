using EntityJoke.Process;
using System;
using System.Reflection;

namespace EntityJoke.Structure
{
    public class Field
    {
        public string ColumnName;
        public string Name;
        public bool IsProperty;
        public bool IsEntity;
        public bool IsKey { get { return Name == "Id"; } }
        public Type Type;

        public Field(FieldInfo field)
        {
            this.ColumnName = new NameFieldGenerator(field).Generate();
            this.Name = field.Name;
            this.Type = field.FieldType;

            VerifyIsEntity();
        }

        private void VerifyIsEntity()
        {
            if (IsSystemClass())
                return;

            ColumnName = String.Format("id_{0}", ColumnName);
            IsEntity = true;
        }

        private bool IsSystemClass()
        {
            return Type.FullName.Contains("System.");
        }

        public Field(MethodInfo method)
        {
            this.ColumnName = new NameFieldGenerator(method).Generate();
            this.Name = method.Name.Replace("get_", "");
            this.Type = method.ReturnType;
            this.IsProperty = true;

            VerifyIsEntity();
        }

        public override string ToString()
        {
            return String.Format("[{0}]{1}: {2}", ColumnName, Name, Type);
        }
    }
}
