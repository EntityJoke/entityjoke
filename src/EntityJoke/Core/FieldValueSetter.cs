using EntityJoke.Structure;
using System.Reflection;

namespace EntityJoke.Core
{
    internal class FieldValueSetter
    {
        protected object obj;
        protected Field field;
        protected object value;

        public FieldValueSetter(object obj, Field field, object value)
        {
            this.obj = obj;
            this.field = field;
            this.value = value;
        }

        public void Set()
        {
            if (field.IsProperty)
                SetPropertyMethod(field.Name);
            else
                SetField(field.Name);
        }

        private void SetPropertyMethod(string fieldName)
        {
            PropertyInfo property = obj.GetType().GetProperty(fieldName);
            property.SetValue(obj, value);
        }

        private void SetField(string fieldName)
        {
            FieldInfo field = obj.GetType().GetField(fieldName);
            field.SetValue(obj, value);
        }
    }
}
