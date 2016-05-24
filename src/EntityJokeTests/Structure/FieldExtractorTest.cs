using EntityJoke.Structure.Fields;
using NUnit.Framework;
using System.Collections.Generic;

namespace EntityJokeTests.Core
{
    [TestFixture]
    public class FieldExtractorTest
    {
        FieldExtractor target;

        [SetUp]
        public void SetUp()
        {
            target = new FieldExtractor(typeof(Produto));
        }

        [Test]
        public void Extract5Fields()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields.Count, Is.EqualTo(5));
        }

        [Test]
        public void ExtractFieldId()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["id"].Name, Is.EqualTo("Id"));
            Assert.That(fields["id"].Type.Name, Is.EqualTo("Int32"));
        }

        [Test]
        public void AssertFieldCodigoDeBarra()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["codigo_de_barras"].Name, Is.EqualTo("CodigoDeBarras"));
            Assert.That(fields["codigo_de_barras"].Type.Name, Is.EqualTo("String"));
        }

        [Test]
        public void AssertFieldNome()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["nome"].Name, Is.EqualTo("Nome"));
            Assert.That(fields["nome"].Type.Name, Is.EqualTo("String"));
        }

        [Test]
        public void AssertFieldQuantidade()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["quantidade"].Name, Is.EqualTo("Quantidade"));
            Assert.That(fields["quantidade"].Type.Name, Is.EqualTo("Double"));
        }

        [Test]
        public void AssertFieldNome2()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["nome2"].Name, Is.EqualTo("Nome2"));
            Assert.That(fields["nome2"].Type.Name, Is.EqualTo("String"));
        }

        [Test]
        public void AssertFieldIdPrecoProduto()
        {
            target = new FieldExtractor(typeof(ProductPrice));

            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["id"].Name, Is.EqualTo("Id"));
            Assert.That(fields["id"].Type.Name, Is.EqualTo("Int32"));
        }

        [Test]
        public void ExtractInitDateFieldOfProductPrice()
        {
            target = new FieldExtractor(typeof(ProductPrice));

            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["init_date"].Name, Is.EqualTo("InitDate"));
            Assert.That(fields["init_date"].Type.Name, Is.EqualTo("DateTime"));
        }

        [Test]
        public void ExtractEndDateFieldOfProductPrice()
        {
            target = new FieldExtractor(typeof(ProductPrice));

            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["end_date"].Name, Is.EqualTo("EndDate"));
            Assert.That(fields["end_date"].Type.Name, Is.EqualTo("DateTime"));
            Assert.That(fields["end_date"].IsEntity, Is.EqualTo(false));
        }

        [Test]
        public void ExtractPriceFieldOfProductPrice()
        {
            target = new FieldExtractor(typeof(ProductPrice));

            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["price"].Name, Is.EqualTo("Price"));
            Assert.That(fields["price"].Type.Name, Is.EqualTo("Double"));
            Assert.That(fields["price"].IsEntity, Is.EqualTo(false));
        }

        [Test]
        public void ExtractProductFieldOfProductPrice()
        {
            target = new FieldExtractor(typeof(ProductPrice));

            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["id_product"].Name, Is.EqualTo("Product"));
            Assert.That(fields["id_product"].Type.Name, Is.EqualTo("ProductForTest"));
            Assert.That(fields["id_product"].IsEntity, Is.EqualTo(true));
        }

    }
}
