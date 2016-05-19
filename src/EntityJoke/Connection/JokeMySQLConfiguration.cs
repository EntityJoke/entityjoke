using System;
using System.Reflection;

namespace EntityJoke
{
    public class JokeMySQLConfiguration : JokeConfiguration
    {

        public override Type DbConnectionType()
        {
            return GetAssembly().GetType("MySql.Data.MySqlClient.MySqlConnection");
        }

        private static Assembly GetAssembly()
        {
            return Assembly.Load("MySql.Data");
        }

        public override Type DbDataAdapterType()
        {
            return GetAssembly().GetType("MySql.Data.MySqlClient.MySqlDataAdapter");
        }

        public override Type DbCommandType()
        {
            return GetAssembly().GetType("MySql.Data.MySqlClient.MySqlCommand");
        }

        public override string ConnectionString()
        {
            return $"server={Host};userid={Username};password={Password};database={DataBase}";
        }
    }
}
