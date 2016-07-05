using EntityJoke.Core;
using EntityJoke.Core.Loaders;
using EntityJoke.Structure.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;

namespace EntityJokeTests.Core
{
    [TestFixture]
    public class EntityLoaderEntidadeCompostaTest
    {
        Entity entity;
        EntityLoaderBuilder target;
        List<Dictionary<string, object>> dataTable;

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
                .Row(dataTable[0])
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
                .Row(dataTable[0])
                .Build();

            var produto2 = (ProductForTest)target
                .Entity(entity)
                .PointerIndexColumn(new PointerIndexColumn())
                .Row(dataTable[1])
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
            dataTable = new List<Dictionary<string, object>>();
            AddRow(1, "Trigo", "Alvalade", "1", "Pacote", "Kg", 2, "Cereal 1");
            AddRow(2, "Trigo", "Sol", "5", "Pacote", "Kg", 2, "Cereal 1");
        }

        private void AddRow(int id, string nome, string marca, string quantidade, string embalagem, string unidadeMedida, int c_id, string c_nome)
        {
            var row1 = new Dictionary<string, object>();
            row1["p_id"      ] = id;
            row1["p_name"    ] = nome;
            row1["p_maker"   ] = marca;
            row1["p_quantity"] = quantidade;
            row1["p_packing" ] = embalagem;
            row1["p_unit"    ] = unidadeMedida;
            row1["c_id"      ] = c_id;
            row1["c_name"    ] = c_nome;
            dataTable.Add(row1);
        }

    }
}
