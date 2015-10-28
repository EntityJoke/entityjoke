using EntityJoke.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityJokeTests.Core
{
    public class CategoriaTeste
    {
        public int Id;
        public string Nome;
        //public int IdCategoriaTeste;
        public IEnumerable<CategoriaTeste> SubCategorias { get { return subCategorias; } }
        private IEnumerable<CategoriaTeste> subCategorias;

        internal void CarregaSubCategorias()
        {
            var sql = new Joke<CategoriaTeste>().Query()
                .Where("categoriaTeste.IdCategoriaTeste = " + Id);

            var lis = sql.Execute();
            subCategorias = lis;
        }
    }
}
