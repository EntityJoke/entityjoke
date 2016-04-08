using EntityJoke.Structure;

namespace EntityJoke.Core
{
    internal class FieldBoolValueSetter : FieldValueSetter
    {

        public FieldBoolValueSetter(object obj, Field field, object value)
            : base(obj, field, value)
        {
            this.value = BoolValue(value);
        }

        private static bool BoolValue(object value)
        {
            return ((int)value) != 0;
        }
    }
}