using EntityJoke.Core;
using EntityJoke.Process.Commands.Executors;
using EntityJokeTests.EntidadesTestes;
using NUnit.Framework;
using System.Collections.Generic;

namespace EntityJokeTests.Process.Commands.Executors
{
    public class InsertCommandsExecutorTest
    {
        CommandsExecutor target;

        List<Dictionary<string, object>> dataTable;

        [SetUp]
        public void SetUp()
        {
            dataTable = new List<Dictionary<string, object>>();
            SetUpDictionaryEntityes();
            DictionaryInstanceFactory.SetDataTableGeneratorMock(true);
        }

        [Test]
        public void InsereContatoComSucesso()
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

        private void AddRowToTable(int id)
        {
            var row1 = new Dictionary<string, object>();
            row1["id"] = id;
            dataTable.Add(row1);
        }

    }
}
