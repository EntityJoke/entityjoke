using EntityJoke;
using EntityJoke.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Connection
{
    [TestFixture]
    public class JokeConfigurationTest
    {
        const string LOCAL_HOST = "localhost";
        const string PORTA_5432 = "5432";
        const string USER       = "user";
        const string SENHA      = "12345";
        const string JOKE_BD    = "jokebd";

        JokeConfiguration target;

        [Test]
        public void SetConexaoParaPostgreSql()
        {
            string connString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};"
                , LOCAL_HOST, PORTA_5432, USER, SENHA, JOKE_BD);

            JokeConfigurationBuilder.NewConfigurationToPostgreSQL()
                .Host(LOCAL_HOST)
                .Port(PORTA_5432)
                .Username(USER)
                .Password(SENHA)
                .DataBase(JOKE_BD)
                .BuildConfiguration();

            target = JokeConfiguration.Get();

            Assert.That(target.ConnectionString(), Is.EqualTo(connString));
        }

    }
}
