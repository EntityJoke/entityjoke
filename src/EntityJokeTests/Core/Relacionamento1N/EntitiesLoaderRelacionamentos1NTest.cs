using EntityJoke.Core;
using EntityJokeTests.EntidadesTestes.Relacionamento1N;
using NUnit.Framework;
using System.Data;

namespace EntityJokeTests.Core.Relacionamento1N
{
    public class EntitiesLoaderRelacionamentos1NTest
    {
        DataTable autorTable;
        DataTable livroTable;

        [SetUp]
        public void SetUp()
        {
            SetUpDictionaryEntityes();
            SetUpDataTable();
        }

        [Test]
        public void Abc()
        {
            DictionaryInstanceFactory.GetInstance().Set("SQLCommandExecutorMock", true);

            DictionaryInstanceFactory.AddDataTableMock(autorTable);
            DictionaryInstanceFactory.AddDataTableMock(livroTable);

            var l = Joke.Query<Autor>()
                .Execute();

            Assert.That(l.Count, Is.EqualTo(1));
            Assert.That(l[0].Id, Is.EqualTo(1));
            Assert.That(l[0].Nome, Is.EqualTo("Érico Veríssimo"));

            Assert.That(l[0].Livros.Count, Is.EqualTo(3));

            DictionaryInstanceFactory.GetInstance().Set("SQLCommandExecutorMock", false);
        }

        private static void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.INSTANCE.Clear();
            DictionaryEntitiesMap.INSTANCE.AddEntity(typeof(Autor));
            DictionaryEntitiesObjects.GetInstance().Clear();
        }

        [TearDown]
        public void TearDown()
        {
            DictionaryInstanceFactory.GetInstance().Set("SQLCommandExecutorMock", false);
        }

        private void SetUpDataTable()
        {
            SetUpAutor();
            SetUpLivro();
        }

        private void SetUpAutor()
        {
            autorTable = new DataTable();
            SetUpColunasAutorTable();
            AddRowAutor(new Autor() { Id = 1, Nome = "Érico Veríssimo" });
        }

        private void SetUpColunasAutorTable()
        {
            autorTable.Columns.Add(new DataColumn("a_id", typeof(int)));
            autorTable.Columns.Add(new DataColumn("a_nome", typeof(string)));
        }

        private void AddRowAutor(Autor autor)
        {
            DataRow row1 = autorTable.NewRow();
            row1["a_id"] = autor.Id;
            row1["a_nome"] = autor.Nome;
            autorTable.Rows.Add(row1);
        }

        private void SetUpLivro()
        {
            livroTable = new DataTable();
            SetUpColunasLivroTable();
            var autor = new Autor() { Id = 1, Nome = "Érico Veríssimo" };
            AddRowLivro(1, "O Continente", autor);
            AddRowLivro(2, "O Retrato", autor);
            AddRowLivro(3, "O Arquipélago", autor);
        }

        private void SetUpColunasLivroTable()
        {
            livroTable.Columns.Add(new DataColumn("l_id", typeof(int)));
            livroTable.Columns.Add(new DataColumn("l_id_autor", typeof(int)));
            livroTable.Columns.Add(new DataColumn("l_nome", typeof(string)));
            livroTable.Columns.Add(new DataColumn("a_id", typeof(int)));
            livroTable.Columns.Add(new DataColumn("a_nome", typeof(string)));
        }

        private void AddRowLivro(int id, string nome, Autor autor)
        {
            DataRow row1 = livroTable.NewRow();
            row1["l_id"] = id;
            row1["l_id_autor"] = autor.Id;
            row1["l_nome"] = nome;
            row1["a_id"] = autor.Id;
            row1["a_nome"] = autor.Nome;
            livroTable.Rows.Add(row1);
        }
    }
}
