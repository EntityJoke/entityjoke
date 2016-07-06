using System.Data.Common;
using System.Reflection;

namespace EntityJoke.Connection
{
    public class DbDataAdapterFactory
    {
        private readonly DbConnection connection;
        private readonly string command;

        public DbDataAdapterFactory(string command, DbConnection connection)
        {
            this.command = command;
            this.connection = connection;
        }

        public object Get()
        {
            return CreateDbDataAdapter();
        }

        private object CreateDbDataAdapter()
        {
            return (object)DbDataAdapterConstructor().
                Invoke(new object[] { command, connection });
        }

        private ConstructorInfo DbDataAdapterConstructor()
        {
            return JokeConfiguration.Get()
                .DbDataAdapterType()
                .GetConstructor(new[] { typeof(string), connection.GetType() });
        }

    }
}
