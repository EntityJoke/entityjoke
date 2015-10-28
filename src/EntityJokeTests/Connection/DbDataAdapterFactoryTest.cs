using EntityJoke.Connection;
using EntityJoke.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Connection
{
    [TestFixture]
    public class DbDataAdapterFactoryTest
    {

        const string SQL = "SELECT * FROM produto_teste";

        //[Test]
        public void MontaConexaoPostgreSQL()
        {
            JokeConfigurationBuilder.NewConfigurationToPostgreSQL()
                .BuildConfiguration();
            
            DbConnection connection = new DbConnectionFactory().Get();
            DbDataAdapter dbDataAdapter = new DbDataAdapterFactory(SQL, connection).Get();

            Assert.That(dbDataAdapter.GetType().ToString(), Is.EqualTo("Npgsql.NpgsqlDataAdapter"));
        }

        //[Test]
        public void MontaConexaoMySQL()
        {
            JokeConfigurationBuilder.NewConfigurationToMySQL()
                .BuildConfiguration();

            DbConnection connection = new DbConnectionFactory().Get();
            DbDataAdapter dbDataAdapter = new DbDataAdapterFactory(SQL, connection).Get();

            Assert.That(dbDataAdapter.GetType().ToString(), Is.EqualTo("MySql.Data.MySqlClient.MySqlDataAdapter"));
        }

    }
}
