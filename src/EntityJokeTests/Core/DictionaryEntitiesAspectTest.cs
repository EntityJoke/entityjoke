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
    public class DictionaryEntitiesAspectTest
    {

        DictionaryEntitiesAspect target;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(ProdutoTeste));
            target = DictionaryEntitiesAspect.GetInstance();
            target.Clear();
        }

        [Test]
        public void AdicionaInstanciaCategoriaTesteAoDicionario()
        {
            Assert.That(target.CountObjects, Is.EqualTo(0));

            CategoriaTeste cat = new CategoriaTeste();
            cat.Id = 1;
            cat.Nome = "Categoria";

            target.TryAddObject(cat);

            Assert.That(target.CountObjects, Is.EqualTo(1));
        }

        [Test]
        public void AdicionaInstanciaCategoriaTesteAoDicionarioUmaUnicaVez()
        {
            Assert.That(target.CountObjects, Is.EqualTo(0));

            CategoriaTeste cat = new CategoriaTeste();
            cat.Id = 1;
            cat.Nome = "Categoria";

            Assert.That(target.TryAddObject(cat), Is.True);
            Assert.That(target.CountObjects, Is.EqualTo(1));

            Assert.That(target.TryAddObject(cat), Is.False);
            Assert.That(target.CountObjects, Is.EqualTo(1));
        }

        [Test]
        public void DicionarioRealizaCopiaDeObjetoCategoria()
        {
            CategoriaTeste cat = new CategoriaTeste();
            cat.Id = 1;
            cat.Nome = "Categoria";

            target.TryAddObject(cat);

            Assert.That(cat, Is.EqualTo(target.GetAspect(cat)));

            target.CopyObjects();

            Assert.That(cat, Is.Not.EqualTo(target.GetAspect(cat)));
        }

        [Test]
        public void DicionarioRealizaCopiaDeObjetoProduto()
        {
            CategoriaTeste cat = new CategoriaTeste();
            cat.Id = 1;
            cat.Nome = "Categoria";

            ProdutoTeste prod1 = new ProdutoTeste() { Id = 1, Nome = "Arroz", CategoriaTeste = cat };
            ProdutoTeste prod2 = new ProdutoTeste() { Id = 2, Nome = "Trigo", CategoriaTeste = cat };

            target.TryAddObject(prod1);
            Assert.That(target.CountObjects, Is.EqualTo(2));
            Assert.That(prod1, Is.EqualTo(target.GetAspect(prod1)));
            Assert.That(cat, Is.EqualTo(target.GetAspect(cat)));

            target.CopyObjects();

            target.TryAddObject(prod2);
            Assert.That(target.CountObjects, Is.EqualTo(3));
            Assert.That(prod2, Is.EqualTo(target.GetAspect(prod2)));
            Assert.That(cat, Is.Not.EqualTo(target.GetAspect(cat)));

            target.CopyObjects();

            Assert.That(target.CountObjects, Is.EqualTo(3));
            Assert.That(cat, Is.Not.EqualTo(target.GetAspect(cat)));
            Assert.That(prod1, Is.Not.EqualTo(target.GetAspect(prod1)));
            Assert.That(prod2, Is.Not.EqualTo(target.GetAspect(prod2)));
        }
    }
}
