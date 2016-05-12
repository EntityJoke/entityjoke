using EntityJoke.Core;
using EntityJoke.Process.Commands;
using EntityJokeTests.Core;
using NUnit.Framework;

namespace EntityJokeTests.Process
{
    [TestFixture]
    public class CommandDeleteGeneratorTest
    {

        DeleteCommandGenerator target;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(CategoriaTeste));
        }

        [Test]
        public void GeraDeleteProduto()
        {
            CategoriaTeste cat = new CategoriaTeste();
            cat.Id = 65;
            cat.Nome = "Cat Delete";

            target = new DeleteCommandGenerator(cat);

            string delete = "DELETE FROM categoria_teste WHERE id = 65";

            Assert.That(target.GetCommand(), Is.EqualTo(delete));

        }

    }
}
