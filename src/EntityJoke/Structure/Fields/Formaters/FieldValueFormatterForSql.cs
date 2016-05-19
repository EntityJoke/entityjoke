namespace EntityJoke.Structure.Fields.Formaters
{
    public class FieldValueFormatterForSql : IFieldValueFormatter
    {
        private readonly object value;

        public FieldValueFormatterForSql(object value)
        {
            this.value = value;
        }

        public string Format()
        {
            return $"'{value}'";
        }

    }
}
