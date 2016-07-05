using EntityJoke.Core;
using EntityJoke.Core.Loaders;
using NUnit.Framework;
using System.Collections.Generic;

namespace EntityJokeTests.Core
{
    [TestFixture]
    public class EntitiesLoaderTest
    {
        EntitiesLoader<Produto> target;

        List<Dictionary<string, object>> produtoTable;

        [SetUp]
        public void SetUp()
        {
            DictionaryEntitiesMap.Clear();
            SetUpDataTable();
            target = new EntitiesLoader<Produto>(produtoTable);
        }

        [Test]
        public void AssertDataTable()
        {
            Assert.That(produtoTable[0]["p_id"], Is.EqualTo(1));
            Assert.That(produtoTable[0]["p_nome"], Is.EqualTo("Trigo"));

            Assert.That(produtoTable[1]["p_id"], Is.EqualTo(2));
            Assert.That(produtoTable[1]["p_nome"], Is.EqualTo("Arroz"));

            Assert.That(produtoTable[2]["p_id"], Is.EqualTo(3));
            Assert.That(produtoTable[2]["p_nome"], Is.EqualTo("Lasanha"));
        }

        [Test]
        public void AssertQuantidadeDeRegistro()
        {
            Assert.That(target.Load().Count, Is.EqualTo(3));
        }

        [Test]
        public void AssertColunaId()
        {
            List<Produto> produtos = target.Load();
            produtos.Add(new Produto() { Id = 1 });
            Assert.That(produtos[0].Id, Is.EqualTo(1));
            Assert.That(produtos[1].Id, Is.EqualTo(2));
            Assert.That(produtos[2].Id, Is.EqualTo(3));
        }

        [Test]
        public void AssertColunaNome()
        {
            List<Produto> produtos = target.Load();
            Assert.That(produtos[0].Nome, Is.EqualTo("Trigo"));
            Assert.That(produtos[1].Nome, Is.EqualTo("Arroz"));
            Assert.That(produtos[2].Nome, Is.EqualTo("Lasanha"));
        }

        [Test]
        public void AssertColunaNome2()
        {
            List<Produto> produtos = target.Load();
            Assert.That(produtos[0].Nome2, Is.EqualTo("Trigo Especial Alvalade"));
            Assert.That(produtos[1].Nome2, Is.EqualTo("Arroz Parboilizado Tio João"));
            Assert.That(produtos[2].Nome2, Is.EqualTo("Lasanha 4 Queijos Saida"));
        }

        [Test]
        public void AssertColunaQuantidade()
        {
            List<Produto> produtos = target.Load();
            Assert.That(produtos[0].Quantidade, Is.EqualTo(1));
            Assert.That(produtos[1].Quantidade, Is.EqualTo(1));
            Assert.That(produtos[2].Quantidade, Is.EqualTo(650));
        }

        [Test]
        public void AssertColunaCodigoDeBarras()
        {
            List<Produto> produtos = target.Load();
            Assert.That(produtos[0].CodigoDeBarras, Is.EqualTo("654987"));
            Assert.That(produtos[1].CodigoDeBarras, Is.EqualTo("652314"));
            Assert.That(produtos[2].CodigoDeBarras, Is.EqualTo("621457"));
        }

        private void SetUpDataTable()
        {
            produtoTable = new List<Dictionary<string, object>>();
            AddRow(1, "Trigo", "Trigo Especial Alvalade", 1, "654987");
            AddRow(2, "Arroz", "Arroz Parboilizado Tio João", 1, "652314");
            AddRow(3, "Lasanha", "Lasanha 4 Queijos Saida", 650, "621457");
        }

        private void AddRow(int id, string nome, string nome2, double quantidade, string codigoDeBarras)
        {
            var row1 = new Dictionary<string, object>();
            row1["p_id"] = id;
            row1["p_nome"] = nome;
            row1["p_nome2"] = nome2;
            row1["p_quantidade"] = quantidade;
            row1["p_codigo_de_barras"] = codigoDeBarras;

            produtoTable.Add(row1);
        }
    }
}
