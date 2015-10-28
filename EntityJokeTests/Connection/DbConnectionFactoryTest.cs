using EntityJoke.Connection;
using EntityJoke.Core;
using NUnit.Framework;
using System.Data.Common;

namespace EntityJokeTests.Connection
{
    [TestFixture]
    public class DbConnectionFactoryTest
    {

        //[Test]
        public void MontaConexaoPostgreSQL()
        {
            JokeConfigurationBuilder.NewConfigurationToPostgreSQL()
                .BuildConfiguration();

            DbConnection connection = new DbConnectionFactory().Get();
            Assert.That(connection.GetType().ToString(), Is.EqualTo("Npgsql.NpgsqlConnection"));
        }

        //[Test]
        public void MontaConexaoMySQL()
        {
            JokeConfigurationBuilder.NewConfigurationToMySQL()
                .BuildConfiguration();

            DbConnection connection = new DbConnectionFactory().Get();
            Assert.That(connection.GetType().ToString(), Is.EqualTo("MySql.Data.MySqlClient.MySqlConnection"));
        }

        //[Test]
        public void MontaConexaoSQLite()
        {
            JokeConfigurationBuilder.NewConfigurationToSQLite()
                .BuildConfiguration();

            DbConnection connection = new DbConnectionFactory().Get();
            Assert.That(connection.GetType().ToString(), Is.EqualTo("System.Data.SQLite.SQLiteConnection"));
        }

    }

}
