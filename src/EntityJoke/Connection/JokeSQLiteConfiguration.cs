using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EntityJoke
{
    public class JokeSQLiteConfiguration : JokeConfiguration
    {

        public override Type DbConnectionType()
        {
            return GetAssembly().GetType("System.Data.SQLite.SQLiteConnection");
        }

        private Assembly GetAssembly()
        {
            return Assembly.Load("System.Data.SQLite");
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
            return String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};"
                , Host, Port, Username, Password, DataBase);
        }
    }
}
