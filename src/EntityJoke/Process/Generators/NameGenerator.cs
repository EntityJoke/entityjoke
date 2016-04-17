
namespace EntityJoke.Process.Generators
{
    internal class NameGenerator
    {
        internal static NameGenerator INSTANCE = new NameGenerator();

        private NameGenerator() { }

        internal string Generate(string name)
        {
            string newName = GetFirstLetter(name);

            foreach (char letter in name.Substring(1))
                newName += ProcessLetter(letter);

            return newName;
        }

        private static string GetFirstLetter(string name)
        {
            return name.Substring(0, 1).ToLower();
        }

        private string ProcessLetter(char letter)
        {
            if (IsTheBeginningWord(letter))
                return GetLetterWithSeparator(letter);

            return letter.ToString();
        }

        private static string GetLetterWithSeparator(char letter)
        {
            return "_" + (char)(letter + 32);
        }

        private bool IsTheBeginningWord(char letter)
        {
            return IsUpperCase(letter);
        }

        private bool IsUpperCase(char letter)
        {
            return letter > 64 && letter < 91;
        }
    }
}
