using EntityJoke.Core;
using EntityJoke.Core.Loaders;
using EntityJoke.Structure.Entities;
using NUnit.Framework;
using System.Data;

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
            ProductForTest produto;

            produto = (ProductForTest)target
                .Entity(entity)
                .Row(dataTable.Rows[0])
                .Columns(dataTable.Columns)
                .Build();

            Assert.That(produto.Id, Is.EqualTo(1));
            Assert.That(produto.Name, Is.EqualTo("Trigo"));
            Assert.That(produto.Maker, Is.EqualTo("Alvalade"));
            Assert.That(produto.Quantity, Is.EqualTo("1"));
            Assert.That(produto.Packing, Is.EqualTo("Pacote"));
            Assert.That(produto.Unit, Is.EqualTo("Kg"));
            Assert.That(produto.Category.Id, Is.EqualTo(2));
            Assert.That(produto.Category.Name, Is.EqualTo("Cereal 1"));
        }

        [Test]
        public void CarregaEntidadeCategoriaIgualParaOsDoisProdutos()
        {
            var produto1 = (ProductForTest)target
                .Entity(entity)
                .PointerIndexColumn(new PointerIndexColumn())
                .Row(dataTable.Rows[0])
                .Columns(dataTable.Columns)
                .Build();

            var produto2 = (ProductForTest)target
                .Entity(entity)
                .PointerIndexColumn(new PointerIndexColumn())
                .Row(dataTable.Rows[1])
                .Columns(dataTable.Columns)
                .Build();

            Assert.That(produto1, Is.Not.EqualTo(produto2));
            Assert.That(produto1.Category, Is.EqualTo(produto2.Category));
        }

        private void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.Clear();
            DictionaryEntitiesObjects.GetInstance().Clear();
            entity = DictionaryEntitiesMap.Get(typeof(ProductForTest));
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
            dataTable.Columns.Add(new DataColumn("p_id"      , typeof(int)));
            dataTable.Columns.Add(new DataColumn("p_name"    , typeof(string)));
            dataTable.Columns.Add(new DataColumn("p_maker"   , typeof(string)));
            dataTable.Columns.Add(new DataColumn("p_quantity", typeof(string)));
            dataTable.Columns.Add(new DataColumn("p_packing" , typeof(string)));
            dataTable.Columns.Add(new DataColumn("p_unit"    , typeof(string)));
            dataTable.Columns.Add(new DataColumn("c_id"      , typeof(int)));
            dataTable.Columns.Add(new DataColumn("c_name"    , typeof(string)));
        }

        private void AddRow(int id, string nome, string marca, string quantidade, string embalagem, string unidadeMedida, int c_id, string c_nome)
        {
            DataRow row1 = dataTable.NewRow();
            row1["p_id"      ] = id;
            row1["p_name"    ] = nome;
            row1["p_maker"   ] = marca;
            row1["p_quantity"] = quantidade;
            row1["p_packing" ] = embalagem;
            row1["p_unit"    ] = unidadeMedida;
            row1["c_id"      ] = c_id;
            row1["c_name"    ] = c_nome;
            dataTable.Rows.Add(row1);
        }

    }
}
