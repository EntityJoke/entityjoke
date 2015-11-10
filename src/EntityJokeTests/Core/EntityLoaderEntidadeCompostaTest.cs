using EntityJoke.Core;
using EntityJoke.Structure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Core
{
    [TestFixture]
    public class EntityLoaderEntidadeCompostaTest
    {
        EntityLoaderBuilder target;
        DataTable dataTable;
        Entity entity;

        [SetUp]
        public void SetUp()
        {
            SetUpDictionaryEntityes();
            SetUpDataTable();
            target = new EntityLoaderBuilder();
        }

        [Test]
        public void CarregaEntidadeProduto()
        {
            ProdutoTeste produto;

            produto = (ProdutoTeste)target
                .Entity(entity)
                .Row(dataTable.Rows[0])
                .Columns(dataTable.Columns)
                .Build();

            Assert.That(produto.Id, Is.EqualTo(1));
            Assert.That(produto.Nome, Is.EqualTo("Trigo"));
            Assert.That(produto.Marca, Is.EqualTo("Alvalade"));
            Assert.That(produto.Quantidade, Is.EqualTo("1"));
            Assert.That(produto.Embalagem, Is.EqualTo("Pacote"));
            Assert.That(produto.UnidadeMedida, Is.EqualTo("Kg"));
            Assert.That(produto.CategoriaTeste.Id, Is.EqualTo(2));
            Assert.That(produto.CategoriaTeste.Nome, Is.EqualTo("Cereal 1"));
        }

        [Test]
        public void CarregaEntidadeCategoriaIgualParaOsDoisProdutos()
        {
            var produto1 = (ProdutoTeste)target
                .Entity(entity)
                .PointerIndexColumn(new PointerIndexColumn())
                .Row(dataTable.Rows[0])
                .Columns(dataTable.Columns)
                .Build();

            var produto2 = (ProdutoTeste)target
                .Entity(entity)
                .PointerIndexColumn(new PointerIndexColumn())
                .Row(dataTable.Rows[1])
                .Columns(dataTable.Columns)
                .Build();

            Assert.That(produto1, Is.Not.EqualTo(produto2));
            Assert.That(produto1.CategoriaTeste, Is.EqualTo(produto2.CategoriaTeste));
        }

        private void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.INSTANCE.Clear();
            DictionaryEntitiesMap.INSTANCE.AddEntity(typeof(ProdutoTeste));
            DictionaryEntitiesObjects.GetInstance().Clear();
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(typeof(ProdutoTeste));
        }

        private void SetUpDataTable()
        {
            SetUpColunas();
            AddRow(1, "Trigo", "Alvalade", "1", "Pacote", "Kg", 2, "Cereal 1");
            AddRow(2, "Trigo", "Sol", "5", "Pacote", "Kg", 2, "Cereal 1");
        }

        private void SetUpColunas()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("p_id", typeof(int)));
            dataTable.Columns.Add(new DataColumn("p_nome", typeof(string)));
            dataTable.Columns.Add(new DataColumn("p_marca", typeof(string)));
            dataTable.Columns.Add(new DataColumn("p_quantidade", typeof(string)));
            dataTable.Columns.Add(new DataColumn("p_embalagem", typeof(string)));
            dataTable.Columns.Add(new DataColumn("p_unidade_medida", typeof(string)));
            dataTable.Columns.Add(new DataColumn("c_id", typeof(int)));
            dataTable.Columns.Add(new DataColumn("c_nome", typeof(string)));
        }

        private void AddRow(int id, string nome, string marca, string quantidade, string embalagem, string unidadeMedida, int c_id, string c_nome)
        {
            DataRow row1 = dataTable.NewRow();
            row1["p_id"] = id;
            row1["p_nome"] = nome;
            row1["p_marca"] = marca;
            row1["p_quantidade"] = quantidade;
            row1["p_embalagem"] = embalagem;
            row1["p_unidade_medida"] = unidadeMedida;
            row1["c_id"] = c_id;
            row1["c_nome"] = c_nome;
            dataTable.Rows.Add(row1);
        }

    }
}
