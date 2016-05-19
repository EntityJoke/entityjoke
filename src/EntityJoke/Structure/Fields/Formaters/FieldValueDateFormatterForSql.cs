using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Structure.Fields.Formaters
{
    public class FieldValueDateFormatterForSql : IFieldValueFormatter
    {
        private DateTime value;

        public FieldValueDateFormatterForSql(object value)
        {
            this.value = (DateTime)value;
        }

        public string Format()
        {
            var timestampUnix = UnixTimestampFromDateTime(value.ToUniversalTime());
            return String.Format("To_Timestamp({0})", timestampUnix);
        }

        public static long UnixTimestampFromDateTime(DateTime date)
        {
            var unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            return unixTimestamp / TimeSpan.TicksPerSecond;
        }
    }
}
