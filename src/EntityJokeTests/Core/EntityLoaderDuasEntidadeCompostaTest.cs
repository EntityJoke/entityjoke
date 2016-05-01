using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using NUnit.Framework;
using System.Data;

namespace EntityJokeTests.Core
{
    [TestFixture]
    public class EntityLoaderDuasEntidadesCompostasTest
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
            ComparadorCategorias comparador;

            comparador = (ComparadorCategorias)target
                .Entity(entity)
                .Row(dataTable.Rows[0])
                .Columns(dataTable.Columns)
                .Build();

            Assert.That(comparador.Id, Is.EqualTo(1));
            Assert.That(comparador.CodigoComparacao, Is.EqualTo("131"));
            Assert.That(comparador.CategoriaA.Id, Is.EqualTo(1));
            Assert.That(comparador.CategoriaA.Nome, Is.EqualTo("Cat 1"));
            Assert.That(comparador.CategoriaB.Id, Is.EqualTo(2));
            Assert.That(comparador.CategoriaB.Nome, Is.EqualTo("Cat 2"));
        }

        [Test]
        public void ValidaMetodoGetFieldsJoins()
        {
            Assert.That(entity.GetFields().Count, Is.EqualTo(4));

            var joins = entity.GetFieldsJoins();
            Assert.That(joins.Count, Is.EqualTo(2));
            Assert.That(joins[0].ColumnName, Is.EqualTo("id_categoria_a"));
            Assert.That(joins[1].ColumnName, Is.EqualTo("id_categoria_b"));
        }

        private void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.INSTANCE.Clear();
            DictionaryEntitiesObjects.GetInstance().Clear();
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(typeof(ComparadorCategorias));
        }

        private void SetUpDataTable()
        {
            SetUpColunas();
            AddRow(1, "131", 1, "Cat 1", 2, "Cat 2");

            DataRow row1 = dataTable.NewRow();
            row1["c_id"] = 2;
            row1["c_codigo_comparacao"] = "132";
            row1["ca_id"] = 1;
            row1["ca_nome"] = "Cat 1";
            dataTable.Rows.Add(row1);
        }

        private void SetUpColunas()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("c_id", typeof(int)));
            dataTable.Columns.Add(new DataColumn("c_codigo_comparacao", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ca_id", typeof(int)));
            dataTable.Columns.Add(new DataColumn("ca_nome", typeof(string)));
            dataTable.Columns.Add(new DataColumn("cat_id", typeof(int)));
            dataTable.Columns.Add(new DataColumn("cat_nome", typeof(string)));
        }

        private void AddRow(int id, string codigoComparacao, int idCatA, string nomeCatA, int idCatB, string nomeCatB)
        {
            DataRow row1 = dataTable.NewRow();
            row1["c_id"] = id;
            row1["c_codigo_comparacao"] = codigoComparacao;
            row1["ca_id"] = idCatA;
            row1["ca_nome"] = nomeCatA;
            row1["cat_id"] = idCatB;
            row1["cat_nome"] = nomeCatB;
            dataTable.Rows.Add(row1);
        }

    }
}
