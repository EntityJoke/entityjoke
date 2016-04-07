using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.EntidadesTestes
{
    public class Contato
    {
        public int Id;
        public string Nome;
        public bool Ativo;
    }

    public class Pessoa
    {
        public int Id;
        public string Nome;
        public bool Ativo;
        public Contato contato;
    }
}
