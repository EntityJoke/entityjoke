using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EntityJoke
{
    public class JokePostgreSQLConfiguration : JokeConfiguration
    {

        public override Type DbConnectionType()
        {
            return GetAssembly().GetType("Npgsql.NpgsqlConnection");
        }

        private Assembly GetAssembly()
        {
            return Assembly.Load("Npgsql");
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
            return String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};"
                , Host, Port, Username, Password, DataBase);
        }
    }
}
