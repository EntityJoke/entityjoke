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
        Entity entityPrecoProduto;
        Entity entityProduto;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesMap.INSTANCE.TryAddEntity(typeof(PrecoProduto));
            entityPrecoProduto = DictionaryEntitiesMap.INSTANCE.GetEntity(typeof(PrecoProduto));
            entityProduto = DictionaryEntitiesMap.INSTANCE.GetEntity(typeof(Produto));
        }

        [Test]
        public void AssertAliasProduto()
        {
            target = new EntityJoinsGenerator();
            EntityJoin tree = target.Generate(entityProduto);

            Assert.That(tree.Alias.Symbol, Is.EqualTo("p"));
            Assert.That(tree.Field, Is.Null);
            Assert.That(tree.Joins.Any(), Is.False);
        }

        [Test]
        public void AssertAliasPrecoProduto()
        {
            target = new EntityJoinsGenerator();

            EntityJoin treePrecoProduto = target.Generate(entityPrecoProduto);
            Assert.That(treePrecoProduto.Alias.Symbol, Is.EqualTo("p"));
            Assert.That(treePrecoProduto.Field, Is.Null);
            Assert.That(treePrecoProduto.Joins.Count, Is.EqualTo(1));
            
            EntityJoin treeProduto = treePrecoProduto.Joins[0];
            Assert.That(treeProduto.Alias.Symbol, Is.EqualTo("pr"));
            Assert.That(treeProduto.Field.ToString(), Is.EqualTo("[id_produto]Produto: EntityJokeTests.Core.Produto"));
            Assert.That(treeProduto.Joins.Any(), Is.False);
        }

    }
}
