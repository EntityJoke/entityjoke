using System.Reflection;

namespace EntityJoke.Process.Generators
{
    internal class NameFieldGenerator
    {
        private MemberInfo field;

        public NameFieldGenerator(MemberInfo field)
        {
            this.field = field;
        }

        public string Generate()
        {
            return NameGenerator.Generate(field.Name.Replace("get_", ""));
        }
    }
}
