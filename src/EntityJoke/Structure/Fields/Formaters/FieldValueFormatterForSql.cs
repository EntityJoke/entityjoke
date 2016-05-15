using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Structure.Fields.Formaters
{
    public class FieldValueFormatterForSql : IFieldValueFormatter
    {
        private object value;
        
        public FieldValueFormatterForSql(object value)
        {
            this.value = value;
        }

        public string Format()
        {
            return String.Format("'{0}'", value);
        }

    }
}
