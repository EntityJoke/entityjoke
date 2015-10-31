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
    public class DictionaryEntitiesAspectsTest
    {

        DictionaryEntitiesAspects target;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(ProdutoTeste));
            target = DictionaryEntitiesAspects.GetInstance();
            target.Clear();
        }

        [Test]
        public void AdicionaInstanciaCategoriaTesteAoDicionario()
        {
            Assert.That(target.CountObjects, Is.EqualTo(0));

            CategoriaTeste cat = new CategoriaTeste();
            cat.Id = 1;
            cat.Nome = "Categoria";

            target.AddOrRefreshAspect(cat);

            Assert.That(target.CountObjects, Is.EqualTo(1));
        }

        [Test]
        public void AdicionaInstanciaCategoriaTesteAoDicionarioUmaUnicaVez()
        {
            Assert.That(target.CountObjects, Is.EqualTo(0));

            CategoriaTeste cat = new CategoriaTeste();
            cat.Id = 1;
            cat.Nome = "Categoria";

            target.AddOrRefreshAspect(cat);
            Assert.That(target.CountObjects, Is.EqualTo(1));
            Assert.That(cat, Is.Not.EqualTo(target.GetAspect(cat)));

            var aspecto = target.GetAspect(cat) as CategoriaTeste;
            Assert.That(cat.Id, Is.EqualTo(aspecto.Id));
            Assert.That(cat.Nome, Is.EqualTo(aspecto.Nome));

            target.AddOrRefreshAspect(cat);
            Assert.That(target.CountObjects, Is.EqualTo(1));
            Assert.That(cat, Is.Not.EqualTo(target.GetAspect(cat)));

            aspecto = target.GetAspect(cat) as CategoriaTeste;
            Assert.That(cat.Id, Is.EqualTo(aspecto.Id));
            Assert.That(cat.Nome, Is.EqualTo(aspecto.Nome));
        }
    }
}
