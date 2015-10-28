using System.Reflection;

namespace EntityJoke.Process
{
    internal class NameFieldGenerator
    {
        private FieldInfo field;
        private MethodInfo method;

        public NameFieldGenerator(FieldInfo field)
        {
            this.field = field;
        }

        public NameFieldGenerator(MethodInfo method)
        {
            this.method = method;
        }

        public string Generate()
        {
            if (IsField())
                return GenerateNameByField();
            return GenereteNameByMethod(); ;
        }

        private bool IsField()
        {
            return field != null;
        }

        private string GenerateNameByField()
        {
            return NameGenerator.INSTANCE.Generate(field.Name);
        }

        private string GenereteNameByMethod()
        {
            return NameGenerator.INSTANCE.Generate(method.Name.Replace("get_", ""));
        }
    }
}
