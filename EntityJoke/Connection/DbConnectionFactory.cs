using EntityJoke.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Connection
{
    public class DbConnectionFactory
    {

        public DbConnection Get()
        {
            return CreateDbConnection();
        }

        private DbConnection CreateDbConnection()
        {
            return (DbConnection)DbConnectionConstructor().
                Invoke(new object[] { JokeConfiguration.Get().ConnectionString() });
        }

        private ConstructorInfo DbConnectionConstructor()
        {
            return JokeConfiguration.Get()
                .DbConnectionType()
                .GetConstructor(new[] { typeof(string) });
        }
    }
}
