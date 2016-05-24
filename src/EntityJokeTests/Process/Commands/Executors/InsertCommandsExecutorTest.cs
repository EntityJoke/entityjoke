using EntityJoke.Core;
using EntityJoke.Process.Commands.Executors;
using EntityJokeTests.EntidadesTestes;
using NUnit.Framework;
using System.Data;

namespace EntityJokeTests.Process.Commands.Executors
{
    public class InsertCommandsExecutorTest
    {
        CommandsExecutor target;

        DataTable dataTable;

        [SetUp]
        public void SetUp()
        {
            SetUpDictionaryEntityes();
            SetUpDataTable();
            DictionaryInstanceFactory.SetDataTableGeneratorMock(true);
        }

        [Test]
        public void InsereObjetoComSucesso()
        {
            ConfigureIdDataTableMock(15);

            var contato = new Contato() { Nome = "Name Contact", Ativo = true };

            Assert.That(contato.Id, Is.EqualTo(0));
            Assert.That(contato.Nome, Is.EqualTo("Name Contact"));
            Assert.That(contato.Ativo, Is.True);

            target = new InsertCommandsExecutor(contato);
            target.Execute();

            Assert.That(contato.Id, Is.EqualTo(15));
            Assert.That(contato.Nome, Is.EqualTo("Name Contact"));
            Assert.That(contato.Ativo, Is.True);
        }

        private void ConfigureIdDataTableMock(int idObject)
        {
            AddRowToTable(idObject);
            DictionaryInstanceFactory.AddDataTableMock(dataTable);
        }

        private static void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.Clear();
            DictionaryEntitiesObjects.GetInstance().Clear();
        }

        [TearDown]
        public void TearDown()
        {
            DictionaryInstanceFactory.SetDataTableGeneratorMock(false);
        }

        private void SetUpDataTable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("id", typeof(int)));
        }

        private void AddRowToTable(int id)
        {
            DataRow row1 = dataTable.NewRow();
            row1["id"] = id;
            dataTable.Rows.Add(row1);
        }

    }
}
