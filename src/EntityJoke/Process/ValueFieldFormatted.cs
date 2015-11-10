using EntityJoke.Core;
using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityJoke.Process
{
    public class ValueFieldFormatted
    {
        private object objectValue;
        private Field field;

        public ValueFieldFormatted(object objectValue, Field field)
        {
            this.objectValue = objectValue;
            this.field = field;
        }

        public string Format()
        {
            var value = new ValueFieldExtractor(objectValue, field).Extract();

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
