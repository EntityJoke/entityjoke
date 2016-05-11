using EntityJoke.Core;
using EntityJoke.Process.Commands;
using EntityJokeTests.Core;
using NUnit.Framework;

namespace EntityJokeTests.Process
{
    [TestFixture]
    public class NonQueryCommandsExecutorTest
    {
        
        const string LOCAL_HOST = "postgresql01.basedeprodutos1.hospedagemdesites.ws";
        const string PORTA_5432 = "5432";
        const string USER       = "basedeprodutos1";
        const string SENHA      = "TyUT3KxQ82";
        const string JOKE_BD    = "basedeprodutos1";

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(CategoriaTeste));
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(Ags));
            JokeConfigurationBuilder.NewConfigurationToPostgreSQL()
                .Host(LOCAL_HOST)
                .Port(PORTA_5432)
                .Username(USER)
                .Password(SENHA)
                .DataBase(JOKE_BD)
                .BuildConfiguration();
        }

        //[Test]
        public void TestA()
        {
            CategoriaTeste cat = new CategoriaTeste();
            cat.Id = 20000;
            cat.Nome = "Teste Insert";

            var insert = new CommandInsertGenerator(cat).Generate();

            NonQueryCommandsExecutor executor = new NonQueryCommandsExecutor(cat);
            executor.Execute();

            var novaCategoria = Joke.Query<CategoriaTeste>()
                .Where("categoriaTeste.Id = 20000")
                .Execute()[0];

            Assert.That(novaCategoria.Id, Is.EqualTo(20000));
            Assert.That(novaCategoria.Nome, Is.EqualTo("Teste Insert"));
        }

        //[Test]
        public void TestB()
        {
            GgsTeste cat = new GgsTeste();
            cat.Nome = "Teste Insert 4";

            var insert = new CommandInsertGenerator(cat).Generate();

            NonQueryCommandsExecutor executor = new NonQueryCommandsExecutor(cat);
            executor.Execute();

            var novaCategoria = Joke.Query<GgsTeste>()
                .Where("nome ILIKE 'Teste%4'")
                .Execute()[0];

            Assert.That(novaCategoria.Id, Is.EqualTo(23));
            Assert.That(novaCategoria.Nome, Is.EqualTo("Teste Insert 4"));
        }

        //[Test]
        public void TestC()
        {
            GgsTeste cat = new GgsTeste();
            cat.Nome = "Gui";

            Ags ags = new Ags();
            ags.Nome = "Alana";
            ags.Ggs = cat;

            NonQueryCommandsExecutor executor = new NonQueryCommandsExecutor(ags);
            executor.Execute();

            var novaCategoria = Joke.Query<Ags>()
                .Where("ags.Ggs.Nome ILIKE 'Gui'")
                .Execute()[0];

            Assert.That(novaCategoria.Id, Is.EqualTo(14));
            Assert.That(novaCategoria.Nome, Is.EqualTo("Gui"));
        }

    }
}
