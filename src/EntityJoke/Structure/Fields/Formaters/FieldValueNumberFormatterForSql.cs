using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Structure.Fields.Formaters
{
    public class FieldValueNumberFormatterForSql : IFieldValueFormatter
    {
        private object value;

        public FieldValueNumberFormatterForSql(object obj)
        {
            this.value = obj;
        }

        public string Format()
        {
            return value.ToString().Replace(",", ".");
        }
    }
}
