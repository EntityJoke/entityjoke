using EntityJoke.Core;
using EntityJoke.Core.Loaders;
using EntityJoke.Structure.Entities;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace EntityJokeTests.Core
{
    [TestFixture]
    public class EntityLoaderTest
    {
        EntityLoaderBuilder target;
        List<Dictionary<string, object>> dataTable;
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
            CategoryForTest produto;

            produto = (CategoryForTest)target
                .Entity(entity)
                .Row(dataTable[0])
                .Build();

            Assert.That(produto.Id, Is.EqualTo(2));
            Assert.That(produto.Name, Is.EqualTo("Cereal 1"));
        }

        private void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.Clear();
            entity = DictionaryEntitiesMap.Get(typeof(CategoryForTest));
        }

        private void SetUpDataTable()
        {
            dataTable = new List<Dictionary<string, object>>();
            AddRow(2, "Cereal 1");
        }

        private void AddRow(int c_id, string c_nome)
        {
            var row1 = new Dictionary<string, object>();
            row1["c_id"] = c_id;
            row1["c_name"] = c_nome;
            dataTable.Add(row1);
        }

    }
}
