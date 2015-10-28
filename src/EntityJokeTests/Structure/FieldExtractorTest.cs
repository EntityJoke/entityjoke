using EntityJoke.Process;
using EntityJoke.Structure;
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
        public void AcertaQuantidadeDeFields()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields.Count, Is.EqualTo(5));
        }

        [Test]
        public void AssertFieldId()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["id"].Name, Is.EqualTo("Id"));
            Assert.That(fields["id"].Type.Name, Is.EqualTo("Int32"));
            Assert.That(fields["id"].IsProperty, Is.EqualTo(false));
        }

        [Test]
        public void AssertFieldCodigoDeBarra()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["codigo_de_barras"].Name, Is.EqualTo("CodigoDeBarras"));
            Assert.That(fields["codigo_de_barras"].Type.Name, Is.EqualTo("String"));
            Assert.That(fields["codigo_de_barras"].IsProperty, Is.EqualTo(false));
        }

        [Test]
        public void AssertFieldNome()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["nome"].Name, Is.EqualTo("Nome"));
            Assert.That(fields["nome"].Type.Name, Is.EqualTo("String"));
            Assert.That(fields["nome"].IsProperty, Is.EqualTo(true));
        }

        [Test]
        public void AssertFieldQuantidade()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["quantidade"].Name, Is.EqualTo("Quantidade"));
            Assert.That(fields["quantidade"].Type.Name, Is.EqualTo("Double"));
            Assert.That(fields["quantidade"].IsProperty, Is.EqualTo(true));
        }

        [Test]
        public void AssertFieldNome2()
        {
            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["nome2"].Name, Is.EqualTo("Nome2"));
            Assert.That(fields["nome2"].Type.Name, Is.EqualTo("String"));
            Assert.That(fields["nome2"].IsProperty, Is.EqualTo(false));
        }

        [Test]
        public void AssertFieldIdPrecoProduto()
        {
            target = new FieldExtractor(typeof(PrecoProduto));

            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["id"].Name, Is.EqualTo("Id"));
            Assert.That(fields["id"].Type.Name, Is.EqualTo("Int32"));
            Assert.That(fields["id"].IsProperty, Is.EqualTo(true));
        }

        [Test]
        public void AssertFieldDataInicioPrecoProduto()
        {
            target = new FieldExtractor(typeof(PrecoProduto));

            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["data_inicio"].Name, Is.EqualTo("DataInicio"));
            Assert.That(fields["data_inicio"].Type.Name, Is.EqualTo("DateTime"));
            Assert.That(fields["data_inicio"].IsProperty, Is.EqualTo(false));
        }

        [Test]
        public void AssertFieldDataFimPrecoProduto()
        {
            target = new FieldExtractor(typeof(PrecoProduto));

            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["data_fim"].Name, Is.EqualTo("DataFim"));
            Assert.That(fields["data_fim"].Type.Name, Is.EqualTo("DateTime"));
            Assert.That(fields["data_fim"].IsProperty, Is.EqualTo(true));
            Assert.That(fields["data_fim"].IsEntity, Is.EqualTo(false));
        }

        [Test]
        public void AssertFieldPrecoPrecoProduto()
        {
            target = new FieldExtractor(typeof(PrecoProduto));

            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["preco"].Name, Is.EqualTo("Preco"));
            Assert.That(fields["preco"].Type.Name, Is.EqualTo("Double"));
            Assert.That(fields["preco"].IsProperty, Is.EqualTo(true));
            Assert.That(fields["preco"].IsEntity, Is.EqualTo(false));
        }

        [Test]
        public void AssertFieldProdutoPrecoProduto()
        {
            target = new FieldExtractor(typeof(PrecoProduto));

            Dictionary<string, Field> fields = target.Extract();
            Assert.That(fields["id_produto"].Name, Is.EqualTo("Produto"));
            Assert.That(fields["id_produto"].Type.Name, Is.EqualTo("Produto"));
            Assert.That(fields["id_produto"].IsProperty, Is.EqualTo(false));
            Assert.That(fields["id_produto"].IsEntity, Is.EqualTo(true));
        }

    }
}
