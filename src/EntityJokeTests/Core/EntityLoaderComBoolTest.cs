using EntityJoke.Core;
using EntityJoke.Structure;
using EntityJokeTests.EntidadesTestes;
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
    internal class EntityLoaderComBoolTest
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
        public void CarregaEntidadeContatoValidaTrue()
        {
            Contato contato;

            contato = (Contato)target
                .Entity(entity)
                .Row(dataTable.Rows[0])
                .Columns(dataTable.Columns)
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
                .Row(dataTable.Rows[1])
                .Columns(dataTable.Columns)
                .Build();

            Assert.That(contato.Id, Is.EqualTo(2));
            Assert.That(contato.Nome, Is.EqualTo("Nome B"));
            Assert.That(contato.Ativo, Is.False);
        }

        private void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.INSTANCE.Clear();
            DictionaryEntitiesMap.INSTANCE.AddEntity(typeof(Contato));
            DictionaryEntitiesObjects.GetInstance().Clear();
            entity = DictionaryEntitiesMap.INSTANCE.GetEntity(typeof(Contato));
        }

        private void SetUpDataTable()
        {
            SetUpColunas();
            AddRow(1, "Nome A", 1);
            AddRow(2, "Nome B", 0);
        }

        private void SetUpColunas()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("p_id", typeof(int)));
            dataTable.Columns.Add(new DataColumn("p_nome", typeof(string)));
            dataTable.Columns.Add(new DataColumn("p_ativo", typeof(int)));
        }

        private void AddRow(int id, string nome, int marca)
        {
            DataRow row1 = dataTable.NewRow();
            row1["p_id"] = id;
            row1["p_nome"] = nome;
            row1["p_ativo"] = marca;
            dataTable.Rows.Add(row1);
        }
    }
}
