using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Core
{
    public class JokeConfigurationBuilder
    {
        public static JokePostgreSQLConfigurationBuilder NewConfigurationToPostgreSQL()
        {
            return new JokePostgreSQLConfigurationBuilder();
        }

        public static JokeMySQLConfigurationBuilder NewConfigurationToMySQL()
        {
            return new JokeMySQLConfigurationBuilder();
        }

        public static JokeSQLiteConfigurationBuilder NewConfigurationToSQLite()
        {
            return new JokeSQLiteConfigurationBuilder();
        }
    }
}
