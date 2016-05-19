using System.Text;

namespace EntityJoke.Process.Generators
{
    internal class NameGenerator
    {
        internal static NameGenerator INSTANCE = new NameGenerator();

        private NameGenerator() { }

        internal static string Generate(string name)
        {
            var builder = new StringBuilder();
            builder.Append(GetFirstLetter(name));

            foreach (char letter in name.Substring(1))
                builder.Append(ProcessLetter(letter));

            return builder.ToString();
        }

        private static string GetFirstLetter(string name)
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
