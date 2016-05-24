using EntityJoke.Core;
using EntityJoke.Process.Generators;
using EntityJoke.Structure.Entities;
using EntityJokeTests.Core;
using NUnit.Framework;
using System.Linq;

namespace EntityJokeTests.Process
{
    public class EntityJoinTest
    {

        EntityJoinsGenerator target;
        Entity entityProductPrice;
        Entity entityProduct;

        [SetUp]
        public void SetUp()
        {
            entityProductPrice = DictionaryEntitiesMap.Get(typeof(ProductPrice));
            entityProduct      = DictionaryEntitiesMap.Get(typeof(ProductForTest));
        }

        [Test]
        public void GeneratePToProductAlias()
        {
            target = new EntityJoinsGenerator();
            EntityJoin tree = target.Generate(entityProduct);

            Assert.That(tree.Alias.Symbol, Is.EqualTo("p"));
            Assert.That(tree.Field, Is.Null);
            Assert.That(tree.Joins.Any(), Is.True);
        }

        [Test]
        public void GeneratePToProductPriceAliasAndPrToProduct()
        {
            target = new EntityJoinsGenerator();

            EntityJoin treeProductPrice = target.Generate(entityProductPrice);
            Assert.That(treeProductPrice.Alias.Symbol, Is.EqualTo("p"));
            Assert.That(treeProductPrice.Field, Is.Null);
            Assert.That(treeProductPrice.Joins.Count, Is.EqualTo(1));
            
            EntityJoin treeProduct = treeProductPrice.Joins[0];
            Assert.That(treeProduct.Alias.Symbol, Is.EqualTo("pr"));
            Assert.That(treeProduct.Field.ToString(), Is.EqualTo("[id_product]Product: EntityJokeTests.Core.ProductForTest"));
            Assert.That(treeProduct.Joins.Any(), Is.True);
        }

    }
}
