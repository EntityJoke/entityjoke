using EntityJoke.Utils.Converters;

namespace EntityJoke.Structure.Fields.Formaters
{
    public class FieldValueBoolFormatterForSql : IFieldValueFormatter
    {
        private readonly bool value;
        private BoolToStringValueConverter converter;

        public FieldValueBoolFormatterForSql(object value)
        {
            this.value = (bool)value;
        }

        public string Format()
        {
            converter = new BoolToStringValueConverter(value);
            return converter.Convert();
        }
    }
}
