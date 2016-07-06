using System.Data.Common;
using System.Reflection;

namespace EntityJoke.Connection
{
    public class DbConnectionFactory
    {

        public static DbConnection Get() => CreateDbConnection();

        private static DbConnection CreateDbConnection()
        {
            return (DbConnection)DbConnectionConstructor().
                Invoke(new object[] { JokeConfiguration.Get().ConnectionString() });
        }

        private static ConstructorInfo DbConnectionConstructor()
        {
            return JokeConfiguration.Get()
                .DbConnectionType()
                .GetConstructor(new[] { typeof(string) });
        }
    }
}
