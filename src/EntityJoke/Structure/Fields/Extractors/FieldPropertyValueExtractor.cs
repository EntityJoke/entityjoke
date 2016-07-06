using System.Reflection;

namespace EntityJoke.Structure.Fields
{
    public class FieldPropertyValueExtractor : FieldValueExtractor
    {
        public FieldPropertyValueExtractor(object objectValue, Field idField)
            : base(objectValue, idField){}

        public override object Extract()
        {
            var value = TypeExtensions.GetField(ObjectValue().GetType(), NameField());
            return value.GetValue(ObjectValue());
        }
    }
}
