using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EntityJoke
{
    public class JokeMySQLConfiguration : JokeConfiguration
    {

        public override Type DbConnectionType()
        {
            return GetAssembly().GetType("MySql.Data.MySqlClient.MySqlConnection");
        }

        private Assembly GetAssembly()
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
            return String.Format("server={0};userid={1};password={2};database={3}"
                , Host, Username, Password, DataBase);
        }
    }
}
