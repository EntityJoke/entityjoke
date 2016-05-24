using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using EntityJokeTests.Core;
using NUnit.Framework;

namespace EntityJokeTests.Procces
{
    [TestFixture]
    public class DictionaryEntitiesTest
    {
        DictionaryEntitiesMap target;

        [SetUp]
        public void SetUp()
        {
            target = DictionaryEntitiesMap.INSTANCE;
            DictionaryEntitiesMap.Clear();
        }

        [Test]
        public void DicionarioTemQueEstarVazio()
        {
            Assert.That(target.EntityesCount(), Is.EqualTo(0));
        }

        [Test]
        public void DicionarioAdicionaEntityProduto()
        {
            bool isEntityAdded = target.TryAddEntity(typeof(Produto));

            Assert.That(isEntityAdded, Is.EqualTo(true));
            Assert.That(target.EntityesCount(), Is.EqualTo(1));
        }

        [Test]
        public void DicionarioAdicionaEntityProdutoUmaUnicaVez()
        {
            bool isEntityAdded = target.TryAddEntity(typeof(Produto));

            Assert.That(isEntityAdded, Is.EqualTo(true));
            Assert.That(target.EntityesCount(), Is.EqualTo(1));

            isEntityAdded = target.TryAddEntity(typeof(Produto));

            Assert.That(isEntityAdded, Is.EqualTo(false));
            Assert.That(target.EntityesCount(), Is.EqualTo(1));
        }

        [Test]
        public void DictionaryAddClassOnly()
        {
            bool isEntityAdded = target.TryAddEntity(typeof(CategoryForTest));

            Assert.That(isEntityAdded, Is.EqualTo(true));
            Assert.That(target.EntityesCount(), Is.EqualTo(1));

            isEntityAdded = target.TryAddEntity(typeof(ProductForTest));

            Assert.That(isEntityAdded, Is.EqualTo(true));
            Assert.That(target.EntityesCount(), Is.EqualTo(2));
        }

        [Test]
        public void LoadEntityCategoryWhenLoadEntityProductForTest()
        {
            target.TryAddEntity(typeof(ProductForTest));

            Assert.That(target.EntityesCount(), Is.EqualTo(2));

            Entity produto = target.GetEntity(typeof(CategoryForTest));

            Assert.That(produto.Name, Is.EqualTo("category_for_test"));
            Assert.That(produto.FieldDictionary.Count, Is.EqualTo(2));

            Assert.That(produto.FieldDictionary["id"].Name, Is.EqualTo("Id"));
            Assert.That(produto.FieldDictionary["id"].Type.Name, Is.EqualTo("Int32"));

            Assert.That(produto.FieldDictionary["name"].Name, Is.EqualTo("Name"));
            Assert.That(produto.FieldDictionary["name"].Type.Name, Is.EqualTo("String"));
        }

    }
}
