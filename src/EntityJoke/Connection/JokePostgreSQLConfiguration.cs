using System;
using System.Reflection;

namespace EntityJoke
{
    public class JokePostgreSQLConfiguration : JokeConfiguration
    {

        public override Type DbConnectionType()
        {
            return GetAssembly().GetType("Npgsql.NpgsqlConnection");
        }

        private static Assembly GetAssembly()
        {
            var assemblyName = new AssemblyName("Npgsql");
            return Assembly.Load(assemblyName);
        }

        public override Type DbDataAdapterType()
        {
            return GetAssembly().GetType("Npgsql.NpgsqlDataAdapter");
        }

        public override Type DbCommandType()
        {
            return GetAssembly().GetType("Npgsql.NpgsqlCommand");
        }

        public override string ConnectionString()
        {
            return $"Server={Host};Port={Port};User Id={Username};Password={Password};Database={DataBase};";
        }
    }
}
