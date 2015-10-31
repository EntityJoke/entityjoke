using EntityJoke.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;

namespace EntityJokeTests.Core
{
    [TestFixture]
    public class EntitiesLoaderEntidadesCompostasTest
    {
        EntitiesLoader<ProdutoTeste> target;
        DataTable produtoTable;

        [SetUp]
        public void SetUp()
        {
            SetUpDictionaryEntityes();
            SetUpDataTable();
            target = new EntitiesLoader<ProdutoTeste>(produtoTable);
        }

        [Test]
        public void AssertQuantidadeDeRegistro()
        {
            Assert.That(target.Load().Count, Is.EqualTo(3));
        }

        [Test]
        public void AssertColunaId()
        {
            List<ProdutoTeste> produtos = target.Load();
            Assert.That(produtos[0].Id, Is.EqualTo(1));
            Assert.That(produtos[1].Id, Is.EqualTo(2));
        }

        [Test]
        public void AssertColunaNome()
        {
            List<ProdutoTeste> produtos = target.Load();
            Assert.That(produtos[0].Nome, Is.EqualTo("Trigo"));
            Assert.That(produtos[1].Nome, Is.EqualTo("Arroz"));
        }

        [Test]
        public void AssertColunaMarca()
        {
            List<ProdutoTeste> produtos = target.Load();
            Assert.That(produtos[0].Marca, Is.EqualTo("Alvalade"));
            Assert.That(produtos[1].Marca, Is.EqualTo("Tio João"));
        }

        [Test]
        public void AssertColunaQuantidade()
        {
            List<ProdutoTeste> produtos = target.Load();
            Assert.That(produtos[0].Quantidade, Is.EqualTo("1"));
            Assert.That(produtos[1].Quantidade, Is.EqualTo("500"));
        }

        [Test]
        public void AssertColunaEmbalagem()
        {
            List<ProdutoTeste> produtos = target.Load();
            Assert.That(produtos[0].Embalagem, Is.EqualTo("Pacote"));
            Assert.That(produtos[1].Embalagem, Is.EqualTo("Pacote"));
        }

        [Test]
        public void AssertColunaUnidadeMedida()
        {
            List<ProdutoTeste> produtos = target.Load();
            Assert.That(produtos[0].UnidadeMedida, Is.EqualTo("Kg"));
            Assert.That(produtos[1].UnidadeMedida, Is.EqualTo("g"));
        }

        [Test]
        public void AssertEntidadeComposta()
        {
            List<ProdutoTeste> produtos = target.Load();
            Assert.That(produtos[0].CategoriaTeste.Id, Is.EqualTo(2));
            Assert.That(produtos[0].CategoriaTeste.Nome, Is.EqualTo("Cereal 1"));

            Assert.That(produtos[1].CategoriaTeste.Id, Is.EqualTo(3));
            Assert.That(produtos[1].CategoriaTeste.Nome, Is.EqualTo("Cereal 2"));
        }

        [Test]
        public void AssertCategoriasIguais()
        {
            List<ProdutoTeste> produtos = target.Load();

            var prod1 = produtos[0];
            var prod2 = produtos[1];
            var prod3 = produtos[2];

            Assert.That(prod1.CategoriaTeste, Is.Not.EqualTo(prod2.CategoriaTeste));
            Assert.That(prod2.CategoriaTeste, Is.Not.EqualTo(prod3.CategoriaTeste));

            Assert.That(prod1.CategoriaTeste, Is.EqualTo(prod3.CategoriaTeste));
        }

        private static void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.INSTANCE.Clear();
            DictionaryEntitiesMap.INSTANCE.AddEntity(typeof(ProdutoTeste));
            DictionaryEntitiesAspect.GetInstance().Clear();
        }

        private void SetUpDataTable()
        {
            SetUpColunas();
            AddRow(1, "Trigo", "Alvalade", "1", "Pacote", "Kg", 2, "Cereal 1");
            AddRow(2, "Arroz", "Tio João", "500", "Pacote", "g", 3, "Cereal 2");
            AddRow(3, "Farinha", "Campo Largo", "500", "Pacote", "g", 2, "Cereal 1");
        }

        private void SetUpColunas()
        {
            produtoTable = new DataTable();
            produtoTable.Columns.Add(new DataColumn("p_id", typeof(int)));
            produtoTable.Columns.Add(new DataColumn("p_nome", typeof(string)));
            produtoTable.Columns.Add(new DataColumn("p_marca", typeof(string)));
            produtoTable.Columns.Add(new DataColumn("p_quantidade", typeof(string)));
            produtoTable.Columns.Add(new DataColumn("p_embalagem", typeof(string)));
            produtoTable.Columns.Add(new DataColumn("p_unidade_medida", typeof(string)));
            produtoTable.Columns.Add(new DataColumn("c_id", typeof(int)));
            produtoTable.Columns.Add(new DataColumn("c_nome", typeof(string)));
        }

        private void AddRow(int id, string nome, string marca, string quantidade, string embalagem, string unidadeMedida, int c_id, string c_nome)
        {
            DataRow row1 = produtoTable.NewRow();
            row1["p_id"] = id;
            row1["p_nome"] = nome;
            row1["p_marca"] = marca;
            row1["p_quantidade"] = quantidade;
            row1["p_embalagem"] = embalagem;
            row1["p_unidade_medida"] = unidadeMedida;
            row1["c_id"] = c_id;
            row1["c_nome"] = c_nome;
            produtoTable.Rows.Add(row1);
        }
    }
}
