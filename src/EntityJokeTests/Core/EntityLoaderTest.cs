using EntityJoke.Core;
using EntityJoke.Core.Loaders;
using EntityJoke.Structure.Entities;
using NUnit.Framework;
using System.Data;

namespace EntityJokeTests.Core
{
    [TestFixture]
    public class EntityLoaderTest
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
        public void CarregaEntidadeCategoria()
        {
            CategoriaTeste produto;

            produto = (CategoriaTeste)target
                .Entity(entity)
                .Row(dataTable.Rows[0])
                .Columns(dataTable.Columns)
                .Build();

            Assert.That(produto.Id, Is.EqualTo(2));
            Assert.That(produto.Nome, Is.EqualTo("Cereal 1"));
        }

        private void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.INSTANCE.Clear();
            DictionaryEntitiesMap.INSTANCE.AddEntity(typeof(CategoriaTeste));
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(typeof(CategoriaTeste));
        }

        private void SetUpDataTable()
        {
            SetUpColunas();
            AddRow(2, "Cereal 1");
        }

        private void SetUpColunas()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("c_id", typeof(int)));
            dataTable.Columns.Add(new DataColumn("c_nome", typeof(string)));
        }

        private void AddRow(int c_id, string c_nome)
        {
            DataRow row1 = dataTable.NewRow();
            row1["c_id"] = c_id;
            row1["c_nome"] = c_nome;
            dataTable.Rows.Add(row1);
        }

    }
}
