using System.Reflection;

namespace EntityJoke.Process.Generators
{
    internal class NameFieldGenerator
    {
        private readonly MemberInfo field;
        private NameGenerator nameGenerator;

        public NameFieldGenerator(MemberInfo field)
        {
            this.field = field;
        }

        public string Generate()
        {
            nameGenerator = new NameGenerator(NameReplaced());
            return nameGenerator.Generate();
        }

        private string NameReplaced()
        {
            return field.Name.Replace("get_", "");
        }
    }
}
