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
        public void FiltraFieldsCollectionsERealizaCast()
        {
            var entity = DictionaryEntitiesMap.INSTANCE.GetEntity(typeof(Autor));
            var fields = entity.GetFieldsCollection();

            Assert.That(fields.Count, Is.EqualTo(1));
            Assert.That(fields[0].Type, Is.EqualTo(typeof(Livro)));
            Assert.That(fields[0].ColumnName, Is.EqualTo("livros"));
        }

        [Test]
        public void CarregaAutorComListaDeLivros()
        {
            DictionaryInstanceFactory.GetInstance().Set("DataTableGeneratorMock", true);

            DictionaryInstanceFactory.AddDataTableMock(autorTable);
            DictionaryInstanceFactory.AddDataTableMock(livroTable);

            var autores = Joke.Query<Autor>()
                .Execute();

            Assert.That(autores.Count, Is.EqualTo(1));

            var ericoVerissimo = autores[0];
            Assert.That(ericoVerissimo.Id, Is.EqualTo(1));
            Assert.That(ericoVerissimo.Nome, Is.EqualTo("Érico Veríssimo"));

            Assert.That(ericoVerissimo.Livros.Count, Is.EqualTo(3));

            Assert.That(ericoVerissimo.Livros[0].Id, Is.EqualTo(1));
            Assert.That(ericoVerissimo.Livros[0].Titulo, Is.EqualTo("O Continente"));

            Assert.That(ericoVerissimo.Livros[1].Id, Is.EqualTo(6));
            Assert.That(ericoVerissimo.Livros[1].Titulo, Is.EqualTo("O Retrato"));

            Assert.That(ericoVerissimo.Livros[2].Id, Is.EqualTo(4));
            Assert.That(ericoVerissimo.Livros[2].Titulo, Is.EqualTo("O Arquipélago"));
        }

        private static void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.Clear();
            DictionaryEntitiesObjects.GetInstance().Clear();
        }

        [TearDown]
        public void TearDown()
        {
            DictionaryInstanceFactory.GetInstance().Set("DataTableGeneratorMock", false);
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
            AddRowLivro(6, "O Retrato", autor);
            AddRowLivro(4, "O Arquipélago", autor);
        }

        private void SetUpColunasLivroTable()
        {
            livroTable.Columns.Add(new DataColumn("l_id", typeof(int)));
            livroTable.Columns.Add(new DataColumn("l_titulo", typeof(string)));
            livroTable.Columns.Add(new DataColumn("a_id", typeof(int)));
            livroTable.Columns.Add(new DataColumn("a_nome", typeof(string)));
        }

        private void AddRowLivro(int id, string nome, Autor autor)
        {
            DataRow row1 = livroTable.NewRow();
            row1["l_id"] = id;
            row1["l_titulo"] = nome;
            row1["a_id"] = autor.Id;
            row1["a_nome"] = autor.Nome;
            livroTable.Rows.Add(row1);
        }
    }
}
