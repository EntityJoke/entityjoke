using System.Text;

namespace EntityJoke.Process.Generators
{
    public class NameGenerator
    {
        private readonly string name;

        public NameGenerator(string name)
        {
            this.name = name;
        }

        public string Generate()
        {
            var builder = new StringBuilder();
            builder.Append(GetFirstLetter());

            foreach (char letter in name.Substring(1))
                builder.Append(ProcessLetter(letter));

            return builder.ToString();
        }

        private string GetFirstLetter()
        {
            return name.Substring(0, 1).ToLower();
        }

        private static string ProcessLetter(char letter)
        {
            if (IsTheBeginningWord(letter))
                return GetLetterWithSeparator(letter);

            return letter.ToString();
        }

        private static string GetLetterWithSeparator(char letter)
        {
            return "_" + (char)(letter + 32);
        }

        private static bool IsTheBeginningWord(char letter)
        {
            return IsUpperCase(letter);
        }

        private static bool IsUpperCase(char letter)
        {
            return letter > 64 && letter < 91;
        }
    }
}
