namespace EntityJoke.Structure.Fields.Formaters
{
    public class FieldValueNumberFormatterForSql : IFieldValueFormatter
    {
        private readonly object value;

        public FieldValueNumberFormatterForSql(object obj)
        {
            value = obj;
        }

        public string Format()
        {
            return value.ToString().Replace(",", ".");
        }
    }
}
