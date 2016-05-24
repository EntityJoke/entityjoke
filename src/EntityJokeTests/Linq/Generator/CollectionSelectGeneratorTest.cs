using EntityJoke.Core;
using EntityJoke.Linq.Generator;
using EntityJokeTests.EntidadesTestes.Relacionamento1N;
using NUnit.Framework;

namespace EntityJokeTests.Linq.Generator
{
    public class CollectionSelectGeneratorTest
    {
        CollectionSelectGenerator target;

        [SetUp]
        public void SetUp()
        {
            SetUpDictionaryEntityes();
        }

        [Test]
        public void GeraSelectDeLivroParaAutor()
        {
            object autor = new Autor() { Id = 1107, Nome = "Érico Veríssimo" };
            var entity = DictionaryEntitiesMap.INSTANCE.GetEntity(typeof(Autor));
            var field = entity.GetFieldsCollection()[0];

            target = new CollectionSelectGenerator(autor, field);

            const string sql = "SELECT l.id l_id, l.titulo l_titulo, a.id a_id, a.nome a_nome FROM livro l LEFT JOIN autor a ON (a.id = l.id_autor) WHERE a.id = 1107";
            Assert.That(target.Generate(), Is.EqualTo(sql));
        }

        private static void SetUpDictionaryEntityes()
        {
            DictionaryEntitiesMap.Clear();
            DictionaryEntitiesObjects.GetInstance().Clear();
        }
    }
}
