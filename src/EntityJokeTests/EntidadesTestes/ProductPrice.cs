using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJokeTests.Core
{
    public class ProductPrice
    {
        public int Id {get; set;}
        public ProductForTest Product;
        public double Price { get; set; }
        public DateTime InitDate;
        public DateTime EndDate { get; set; }
    }
}
