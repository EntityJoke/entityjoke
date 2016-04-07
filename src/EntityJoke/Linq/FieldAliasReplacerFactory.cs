using EntityJoke.Structure;

namespace EntityJoke.Linq
{
    public class FieldAliasReplacerFactory
    {

        public static FieldAliasReplacer Get(Field field)
        {
            if (IsBoolField(field))
                return new BoolFieldAliasReplacer();

            return new FieldAliasReplacer();
        }

        private static bool IsBoolField(Field field)
        {
            return field.Type == typeof(bool);
        }
    }
}
