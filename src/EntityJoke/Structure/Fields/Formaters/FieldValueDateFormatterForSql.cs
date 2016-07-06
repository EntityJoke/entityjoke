using System;

namespace EntityJoke.Structure.Fields.Formaters
{
    public class FieldValueDateFormatterForSql : IFieldValueFormatter
    {
        private readonly DateTime value;

        public FieldValueDateFormatterForSql(object value)
        {
            this.value = (DateTime)value;
        }

        public string Format()
        {
            var timestampUnix = UnixTimestampFromDateTime(value.ToUniversalTime());
            return $"To_Timestamp({timestampUnix})";
        }

        public static long UnixTimestampFromDateTime(DateTime date)
        {
            var unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            return unixTimestamp / TimeSpan.TicksPerSecond;
        }
    }
}
