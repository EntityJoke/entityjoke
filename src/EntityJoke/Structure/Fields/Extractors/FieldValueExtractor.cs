using EntityJoke.Structure.Fields;
using System.Reflection;

namespace EntityJoke.Structure.Fields
{
    public class FieldValueExtractor
    {
        private object objectValue;
        private Field field;

        public FieldValueExtractor(object objectValue, Field idField)
        {
            this.objectValue = objectValue;
            this.field = idField;
        }

        public virtual object Extract()
        {
            FieldInfo value = ObjectValue().GetType().GetField(NameField());
            return value.GetValue(objectValue);
        }

        protected object ObjectValue()
        {
            return objectValue;
        }

        protected string NameField()
        {
            return field.Name;
        }
    }
}
