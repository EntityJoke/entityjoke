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
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(CategoryForTest));
        }

        [Test]
        public void GeraDeleteProduto()
        {
            CategoryForTest cat = new CategoryForTest();
            cat.Id = 65;
            cat.Name = "Cat Delete";

            target = new DeleteCommandGenerator(cat);

            string delete = "DELETE FROM category_for_test WHERE id = 65";

            Assert.That(target.GetCommand(), Is.EqualTo(delete));

        }

    }
}
