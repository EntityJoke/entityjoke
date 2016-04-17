using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.EntidadesTestes.Relacionamento1N
{
    internal class Autor
    {
        public int Id;
        public string Nome;
        public List<Livro> Livros = new List<Livro>();
    }
}
