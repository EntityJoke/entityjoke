using EntityJoke.Core;
using EntityJoke.Core.Loaders;
using EntityJoke.Structure.Entities;
using EntityJokeTests.EntidadesTestes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;

namespace EntityJokeTests.Core
{
    [TestFixture]
    internal class EntityLoaderComBoolTest
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
        public void CarregaEntidadeContatoValidaTrue()
        {
            Contato contato;

            contato = (Contato)target
                .Entity(entity)
                .Row(dataTable[0])
                .Build();

            Assert.That(contato.Id   , Is.EqualTo(1));
            Assert.That(contato.Nome , Is.EqualTo("Nome A"));
            Assert.That(contato.Ativo, Is.True);
        }

        [Test]
        public void CarregaEntidadeContatoValidaFalse()
        {
            Contato contato;

            contato = (Contato)target
                .Entity(entity)
                .Row(dataTable[1])
                .Build();

            Assert.That(contato.Id, Is.EqualTo(2));
            Assert.That(contato.Nome, Is.EqualTo("Nome B"));
            Assert.That(contato.Ativo, Is.False);
        }

        private void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.Clear();
            DictionaryEntitiesObjects.GetInstance().Clear();
            entity = DictionaryEntitiesMap.Get(typeof(Contato));
        }

        private void SetUpDataTable()
        {
            dataTable = new List<Dictionary<string, object>>();
            AddRow(1, "Nome A", 1);
            AddRow(2, "Nome B", 0);
        }

        private void AddRow(int id, string nome, int marca)
        {
            var row1 = new Dictionary<string, object>();
            row1["p_id"] = id;
            row1["p_nome"] = nome;
            row1["p_ativo"] = marca;
            dataTable.Add(row1);
        }
    }
}
