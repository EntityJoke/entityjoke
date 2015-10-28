using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityJoke.Linq
{
    public class OrderByGeneratorBuilder : SQLWithConditionGeneratorBuilder<OrderByGeneratorBuilder>
    {

        public override string Build()
        {
            if (condition == null)
                return "";

            return String.Format(" ORDER BY {0}", condition);
        }
    }
}
