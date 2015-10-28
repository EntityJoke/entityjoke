using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Linq
{
    public class QuerySimpleGeneratorFactory
    {
        public static IQuerySimpleGenerator Get()
        {
            return new QueryDataBasePattern();
        }
    }
}
