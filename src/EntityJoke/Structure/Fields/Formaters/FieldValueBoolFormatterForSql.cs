using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Structure.Fields.Formaters
{
    public class FieldValueBoolFormatterForSql : IFieldValueFormatter
    {
        private bool value;
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
