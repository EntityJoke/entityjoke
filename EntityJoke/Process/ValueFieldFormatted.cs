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
            return IsNumber() ? value.ToString() : GetValueText(value);
        }

        private static string GetValueText(object value)
        {
            return String.Format("'{0}'", value);
        }

        private bool IsNumber()
        {
            return field.Type.FullName.StartsWith("System.Int")
                || field.Type.FullName.StartsWith("System.Double");
        }

    }
}
