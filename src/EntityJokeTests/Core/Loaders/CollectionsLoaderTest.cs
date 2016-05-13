using EntityJoke.Core;
using EntityJoke.Core.Loaders;
using EntityJoke.Process;
using EntityJokeTests.EntidadesTestes.Relacionamento1N;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Core.Loaders
{
    public class CollectionsLoaderTest
    {
        DataTable livroTable;

        CollectionsLoader target;

        [SetUp]
        public void SetUp()
        {
            SetUpDictionaryEntityes();
            SetUpDataTable();
            DictionaryInstanceFactory.SetDataTableGeneratorMock(true);
        }

        [Test]
        public void CarregaAutorComListaDeLivros()
        {
            DictionaryInstanceFactory.AddDataTableMock(livroTable);

            var autor = new Autor() { Id = 1, Nome = "Érico Veríssimo" };

            var dictionary = new Dictionary<String, Object>();
            dictionary.Add(new KeyDictionaryObjectExtractor(autor).Extract(), autor);
            target = new CollectionsLoader(autor, dictionary);

            target.Load();

            var livros = autor.Livros;
            Assert.That(livros.Count, Is.EqualTo(3));

            Assert.That(livros[0].Id, Is.EqualTo(1));
            Assert.That(livros[0].Titulo, Is.EqualTo("O Continente"));

            Assert.That(livros[1].Id, Is.EqualTo(6));
            Assert.That(livros[1].Titulo, Is.EqualTo("O Retrato"));

            Assert.That(livros[2].Id, Is.EqualTo(4));
            Assert.That(livros[2].Titulo, Is.EqualTo("O Arquipélago"));
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
            DictionaryInstanceFactory.SetDataTableGeneratorMock(false);
        }

        private void SetUpDataTable()
        {
            SetUpLivro();
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
