using System;
using System.Reflection;

namespace EntityJoke
{
    public class JokeSQLiteConfiguration : JokeConfiguration
    {

        public override Type DbConnectionType()
        {
            return GetAssembly().GetType("System.Data.SQLite.SQLiteConnection");
        }

        private static Assembly GetAssembly()
        {
            var assemblyName = new AssemblyName("System.Data.SQLite");
            return Assembly.Load(assemblyName);
        }

        public override Type DbDataAdapterType()
        {
            return GetAssembly().GetType("System.Data.SQLite.SQLiteDataAdapter");
        }

        public override Type DbCommandType()
        {
            return GetAssembly().GetType("System.Data.SQLite.SQLiteDataAdapter");
        }

        public override string ConnectionString()
        {
            return $"Server={Host};Port={Port};User Id={Username};Password={Password};Database={DataBase};";
        }
    }
}
