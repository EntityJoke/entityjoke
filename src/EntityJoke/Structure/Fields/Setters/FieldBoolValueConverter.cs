namespace EntityJoke.Structure.Fields
{
    internal class FieldBoolValueConverter
    {
        private object value;

        public FieldBoolValueConverter(object value)
        {
            this.value = value;
        }

        internal bool Convert()
        {
            return IsInteger(value) ? ConvertIntToBool(value) : CastToBool(value);
        }

        private bool IsInteger(object value)
        {
            return value is int;
        }

        private bool ConvertIntToBool(object value)
        {
            return ((int)value) != 0;
        }

        private bool CastToBool(object value)
        {
            return (bool)value;
        }

    }
}