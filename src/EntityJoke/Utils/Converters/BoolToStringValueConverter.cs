namespace EntityJoke.Utils.Converters
{
    public class BoolToStringValueConverter
    {
        private readonly bool value;

        public BoolToStringValueConverter(bool value)
        {
            this.value = value;
        }

        public string Convert()
        {
            return value ? "1" : "0";
        }
    }
}
