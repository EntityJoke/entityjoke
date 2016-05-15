using System;

namespace EntityJoke.Structure.Fields
{
    public class FieldValueFormater
    {
        private object objectValue;
        private Field field;

        public FieldValueFormater(object objectValue, Field field)
        {
            this.objectValue = objectValue;
            this.field = field;
        }

        public string Format()
        {
            var value = field.GetExtractor(objectValue).Extract();

            if (IsNumber())
                return value.ToString();

            if (IsDateTime(value))
                return GetDateValue(value);

            return GetValueText(value);
        }

        private bool IsNumber()
        {
            return field.Type.FullName.StartsWith("System.Int")
                || field.Type.FullName.StartsWith("System.Double");
        }

        private bool IsDateTime(object value)
        {
            return field.Type.FullName.StartsWith("System.Date");
        }

        private string GetDateValue(object value)
        {
            return GetValueText(((DateTime)value).GetDateTimeFormats()[54]);
        }

        private static string GetValueText(object value)
        {
            return String.Format("'{0}'", value);
        }

    }
}
