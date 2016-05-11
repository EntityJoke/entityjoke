using EntityJoke.Core;
using EntityJokeTests.EntidadesTestes.Relacionamento1N;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Core.Relacionamento1N
{
    public class TestIntegration
    {

        [Test]
        public void Abc()
        {
            JokeConfigurationBuilder.NewConfigurationToPostgreSQL()
                .Host("postgresql01.basedeprodutos1.hospedagemdesites.ws")
                .Port("5432")
                .Username("basedeprodutos1")
                .Password("TyUT3KxQ82")
                .DataBase("basedeprodutos1")
                .BuildConfiguration();


            var list = Joke.Query<Autor>().Execute();

        }

    }
}
