using EntityJoke.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Core
{
    [TestFixture]
    public class DictionaryEntitiesObjectsTest
    {

        DictionaryEntitiesObjects target;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(ProductForTest));
            target = DictionaryEntitiesObjects.GetInstance();
            target.Clear();
        }

        [Test]
        public void AdicionaInstanciaCategoriaTesteAoDicionario()
        {
            Assert.That(target.CountObjects, Is.EqualTo(0));

            CategoryForTest cat = new CategoryForTest();
            cat.Id = 1;
            cat.Name = "Categoria";

            target.AddOrRefreshObject(cat);

            Assert.That(target.CountObjects, Is.EqualTo(1));
        }

        [Test]
        public void AdicionaInstanciaCategoriaTesteAoDicionarioUmaUnicaVez()
        {
            Assert.That(target.CountObjects, Is.EqualTo(0));

            CategoryForTest cat = new CategoryForTest();
            cat.Id = 1;
            cat.Name = "Categoria";

            target.AddOrRefreshObject(cat);
            Assert.That(target.CountObjects, Is.EqualTo(1));

            target.AddOrRefreshObject(cat);
            Assert.That(target.CountObjects, Is.EqualTo(1));
        }
    }
}
