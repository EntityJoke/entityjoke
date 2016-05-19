namespace EntityJoke.Structure.Fields.Formaters
{
    public class FieldValueBoolFormatterForSql : IFieldValueFormatter
    {
        private readonly bool value;
        //FieldBoolValueConverter converter;

        public FieldValueBoolFormatterForSql(object value)
        {
            this.value = (bool)value;
        }

        public string Format()
        {
            return ""; //converter.Convert().ToString;
        }
    }
}
