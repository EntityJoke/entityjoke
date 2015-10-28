using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityJoke.Connection;

namespace EntityJoke
{
    public class JokeMySQLConfigurationBuilder
    {
        private string host;
        private string port;
        private string username;
        private string password;
        private string dataBase;

        public JokeMySQLConfigurationBuilder Host(string host)
        {
            this.host = host;
            return this;
        }

        public JokeMySQLConfigurationBuilder Port(string port)
        {
            this.port = port;
            return this;
        }

        public JokeMySQLConfigurationBuilder Username(string username)
        {
            this.username = username;
            return this;
        }

        public JokeMySQLConfigurationBuilder Password(string password)
        {
            this.password = password;
            return this;
        }

        public JokeMySQLConfigurationBuilder DataBase(string dataBase)
        {
            this.dataBase = dataBase;
            return this;
        }

        public void BuildConfiguration()
        {
            var config = new JokeMySQLConfiguration();
            config.Host = host;
            config.Port = port;
            config.Username = username;
            config.Password = password;
            config.DataBase = dataBase;

            JokeConfiguration.Set(config);
        }
    }
}
