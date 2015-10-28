using EntityJoke.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace EntityJokeTests.Core
{
    [TestFixture]
    public class JokeTest
    {
        const string LOCAL_HOST = "postgresql01.basedeprodutos1.hospedagemdesites.ws";
        const string PORTA_5432 = "5432";
        const string USER       = "basedeprodutos1";
        const string SENHA      = "TyUT3KxQ82";
        const string JOKE_BD    = "basedeprodutos1";

        Joke<Produto> target;

        [SetUp]
        public void SetUp()
        {
            new Joke<Ags>();
            target = new Joke<Produto>();
            JokeConfigurationBuilder.NewConfigurationToPostgreSQL()
                .Host(LOCAL_HOST)
                .Port(PORTA_5432)
                .Username(USER)
                .Password(SENHA)
                .DataBase(JOKE_BD)
                .BuildConfiguration();
        }

        [Test]
        public void NomeTabelaTemQueSerProdutoMinusculo()
        {
            Assert.That(target.GetEntityName(), Is.EqualTo("produto"));
        }

        [Test]
        public void QuantidadeDeFieldsTemQueSerCinco()
        {
            Assert.That(target.GetEntityFields().Count, Is.EqualTo(5));
        }

        //[Test]
        public void ConsultaProdutosPorId()
        {
            Joke<ProdutoTeste> targetTeste = new Joke<ProdutoTeste>();

            List<ProdutoTeste> produtos = targetTeste.Query()
                .Where("produtoTeste.Id <= 10")
                .Execute();

            Assert.That(produtos.Count, Is.EqualTo(10));
        }

        //[Test]
        public void ConsultaCategoriaPorNome()
        {
            Joke<CategoriaTeste> targetTeste = new Joke<CategoriaTeste>();
            List<CategoriaTeste> produtos = targetTeste.Query()
                .Where("categoriaTeste.Nome = 'Arroz, Massas e Pratos Prontos'")
                .Execute();

            Assert.That(produtos.Count, Is.EqualTo(1));
        }

        //[Test]
        public void ConsultaTodosOsProdutos()
        {
            Joke<ProdutoTeste> targetTeste = new Joke<ProdutoTeste>();
            List<ProdutoTeste> produtos = targetTeste.GetAll();

            Assert.That(produtos.Count, Is.EqualTo(11088));
        }

        //[Test]
        public void ConsultaTodasAsCategoriasPrincipais()
        {
            Joke<CategoriaTeste> targetTeste = new Joke<CategoriaTeste>();
            List<CategoriaTeste> produtos = targetTeste.Query()
                .Where("id_categoria_teste = 0").Execute();

            Assert.That(produtos.Count, Is.EqualTo(27));
        }

        //[Test]
        public void TodosProdutosMySQL()
        {
            JokeConfigurationBuilder.NewConfigurationToMySQL()
                .Host("mysql01.ggs5.hospedagemdesites.ws")
                .Username("ggs5")
                .Password("desenv_1")
                .DataBase("ggs5")
                .BuildConfiguration();

            Joke<ProdutoTeste> targetTeste = new Joke<ProdutoTeste>();
            List<ProdutoTeste> produtos = targetTeste.GetAll();

            Assert.That(produtos.Count, Is.EqualTo(3));
        }

        //[Test]
        public void ConsultaTodasAsCategoriasPrincipaisasdas()
        {
            var asd = EntityJokes.Query<GgsTeste>().Execute();

            var obj = asd[0];

            EntityJokes.Delete(obj);

            Assert.That(obj, Is.Null);
        }

    }

}
