namespace EntityJoke.Structure.Fields
{
    public class FieldValueExtractor
    {
        private object objectValue;
        private Field field;

        public FieldValueExtractor(object objectValue, Field field)
        {
            this.objectValue = objectValue;
            this.field = field;
        }

        public virtual object Extract()
        {
            var value = ObjectValue().GetType().GetField(NameField());
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
