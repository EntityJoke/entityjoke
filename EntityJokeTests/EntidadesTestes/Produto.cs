using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Core
{
    public class Produto
    {
        public int Id;
        public string Nome { get; set; }
        public string Nome2;
        public double Quantidade { get; set; }
        public string CodigoDeBarras;

        public string get_MetodoNaoField()
        { 
            return ""; 
        }

    }
}
