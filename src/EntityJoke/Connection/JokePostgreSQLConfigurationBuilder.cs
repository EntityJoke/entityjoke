using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityJoke.Connection;

namespace EntityJoke
{
    public class JokePostgreSQLConfigurationBuilder
    {
        private string host;
        private string port;
        private string username;
        private string password;
        private string dataBase;

        public JokePostgreSQLConfigurationBuilder Host(string host)
        {
            this.host = host;
            return this;
        }

        public JokePostgreSQLConfigurationBuilder Port(string port)
        {
            this.port = port;
            return this;
        }

        public JokePostgreSQLConfigurationBuilder Username(string username)
        {
            this.username = username;
            return this;
        }

        public JokePostgreSQLConfigurationBuilder Password(string password)
        {
            this.password = password;
            return this;
        }

        public JokePostgreSQLConfigurationBuilder DataBase(string dataBase)
        {
            this.dataBase = dataBase;
            return this;
        }

        public void BuildConfiguration()
        {
            var config = new JokePostgreSQLConfiguration();
            config.Host = host;
            config.Port = port;
            config.Username = username;
            config.Password = password;
            config.DataBase = dataBase;

            JokeConfiguration.Set(config);
        }
    }
}
