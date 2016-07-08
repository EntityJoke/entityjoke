using System.Reflection;

namespace EntityJoke.Structure.Fields
{
    internal class FieldPropertyValueSetter : FieldValueSetter
    {

        public FieldPropertyValueSetter(object obj, Field field, object value)
            : base(obj, field, value) {}

        public override void Set()
        {
            var field = TypeExtensions.GetProperty(obj.GetType(), this.field.Name);
            field.SetValue(obj, value);
        }
    }
}