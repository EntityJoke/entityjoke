using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Core
{
    public class PrecoProduto
    {
        public int Id {get; set;}
        public Produto Produto;
        public double Preco { get; set; }
        public DateTime DataInicio;
        public DateTime DataFim { get; set; }
    }
}
