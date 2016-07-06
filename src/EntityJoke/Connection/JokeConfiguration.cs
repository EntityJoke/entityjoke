using System;

namespace EntityJoke
{
    public abstract class JokeConfiguration
    {
        private static JokeConfiguration instance;

        public string Host;
        public string Port;
        public string Username;
        public string Password;
        public string DataBase;

        public static JokeConfiguration Get()
        {
            return instance;
        }

        public static void Set(JokeConfiguration value)
        {
            instance = value;
        }

        public abstract Type DbConnectionType();
        public abstract Type DbDataAdapterType();
        public abstract Type DbCommandType();
        public abstract string ConnectionString();
    }
}
