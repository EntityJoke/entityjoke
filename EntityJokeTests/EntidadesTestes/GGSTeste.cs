using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Core
{
    public class GgsTeste
    {
        public int Id;
        public string Nome;
    }

    public class Ags
    {
        public int Id;
        public string Nome;
        public GgsTeste Ggs;
    }
}
