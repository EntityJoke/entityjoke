using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityJoke.Connection;

namespace EntityJoke
{
    public class JokeSQLiteConfigurationBuilder
    {
        private string host;
        private string port;
        private string username;
        private string password;
        private string dataBase;

        public JokeSQLiteConfigurationBuilder Host(string host)
        {
            this.host = host;
            return this;
        }

        public JokeSQLiteConfigurationBuilder Port(string port)
        {
            this.port = port;
            return this;
        }

        public JokeSQLiteConfigurationBuilder Username(string username)
        {
            this.username = username;
            return this;
        }

        public JokeSQLiteConfigurationBuilder Password(string password)
        {
            this.password = password;
            return this;
        }

        public JokeSQLiteConfigurationBuilder DataBase(string dataBase)
        {
            this.dataBase = dataBase;
            return this;
        }

        public void BuildConfiguration()
        {
            var config = new JokeSQLiteConfiguration
            {
                Host = host,
                Port = port,
                Username = username,
                Password = password,
                DataBase = dataBase
            };

            JokeConfiguration.Set(config);
        }
    }
}
