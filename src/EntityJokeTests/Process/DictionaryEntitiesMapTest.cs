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
            target.Clear();
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
        public void DicionarioAdicionaDuasClasses()
        {
            bool isEntityAdded = target.TryAddEntity(typeof(Produto));

            Assert.That(isEntityAdded, Is.EqualTo(true));
            Assert.That(target.EntityesCount(), Is.EqualTo(1));

            isEntityAdded = target.TryAddEntity(typeof(PrecoProduto));

            Assert.That(isEntityAdded, Is.EqualTo(true));
            Assert.That(target.EntityesCount(), Is.EqualTo(2));
        }

        [Test]
        public void RecuperaEntityProdutoAposCarregarPrecoProduto()
        {
            target.TryAddEntity(typeof(PrecoProduto));

            Assert.That(target.EntityesCount(), Is.EqualTo(2));

            Entity produto = target.GetEntity(typeof(Produto));

            Assert.That(produto.Name, Is.EqualTo("produto"));
            Assert.That(produto.FieldDictionary.Count, Is.EqualTo(5));

            Assert.That(produto.FieldDictionary["id"].Name, Is.EqualTo("Id"));
            Assert.That(produto.FieldDictionary["id"].Type.Name, Is.EqualTo("Int32"));

            Assert.That(produto.FieldDictionary["codigo_de_barras"].Name, Is.EqualTo("CodigoDeBarras"));
            Assert.That(produto.FieldDictionary["codigo_de_barras"].Type.Name, Is.EqualTo("String"));
        }

    }
}
