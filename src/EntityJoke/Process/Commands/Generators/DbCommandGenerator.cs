using System.Data.Common;
using System.Reflection;

namespace EntityJoke.Process.Commands.Generators
{
    public class DbCommandGenerator
    {

        public static DbCommand Generate(string sql) => CreateDbConnection(sql);

        private static DbCommand CreateDbConnection(string sql)
        {
            return (DbCommand)DbConnectionConstructor().
                Invoke(new object[] { sql });
        }

        private static ConstructorInfo DbConnectionConstructor()
        {
            return JokeConfiguration.Get()
                .DbCommandType()
                .GetConstructor(new[] { typeof(string) });
        }

    }
}
