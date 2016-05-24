using EntityJoke.Core;
using System.Collections.Generic;

namespace EntityJokeTests.Core
{
    public class CategoryForTest
    {
        public int Id;
        public string Name;
        //public int IdCategoriaTeste;
        public IEnumerable<CategoryForTest> SubCategories { get { return subCategories; } }
        private IEnumerable<CategoryForTest> subCategories;

        internal void CarregaSubCategorias()
        {
            var sql = Joke.Query<CategoryForTest>()
                .Where("categoriaTeste.IdCategoriaTeste = " + Id);

            var lis = sql.Execute();
            subCategories = lis;
        }
    }
}
