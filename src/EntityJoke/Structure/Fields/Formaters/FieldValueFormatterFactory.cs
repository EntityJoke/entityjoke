using System;

namespace EntityJoke.Structure.Fields.Formaters
{
    public class FieldValueFormatterFactory
    {
        public static IFieldValueFormatter NewInstance(Object value)
        {
            if (IsStringValue(value))
                return new FieldValueFormatterForSql(value);

            if (IsDateValue(value))
                return new FieldValueDateFormatterForSql(value);

            if (IsBoolValue(value))
                return new FieldValueBoolFormatterForSql(value);

            return new FieldValueNumberFormatterForSql(value);
        }

        private static bool IsStringValue(object value)
        {
            return value is string;
        }

        private static bool IsDateValue(object value)
        {
            return value is DateTime;
        }

        private static bool IsBoolValue(object value)
        {
            return value is bool;
        }
    }
}
