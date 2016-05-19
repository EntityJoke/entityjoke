namespace EntityJoke.Structure.Fields
{
    public class FieldValueSetter
    {
        protected object obj;
        protected Field field;
        protected object value;

        public FieldValueSetter(object obj, Field field, object value)
        {
            this.obj = obj;
            this.field = field;
            this.value = GetValue(value);
        }

        private object GetValue(object value)
        {
            return IsBool() ? BoolValue(value) : value;
        }

        private bool IsBool()
        {
            return typeof(bool) == field.Type;
        }

        private static bool BoolValue(object value)
        {
            return new FieldBoolValueConverter(value).Convert();;
        }

        public virtual void Set()
        {
            var field = obj.GetType().GetField(this.field.Name);
            field.SetValue(obj, value);
        }

    }
}
