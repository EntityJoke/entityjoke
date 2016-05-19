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
            return $"Server={Host};Port={Port};User Id={Username};Password={Password};Database={DataBase};";
        }
    }
}
