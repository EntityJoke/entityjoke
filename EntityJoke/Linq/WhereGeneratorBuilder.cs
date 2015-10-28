using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Linq
{
    public class WhereGeneratorBuilder : SQLWithConditionGeneratorBuilder<WhereGeneratorBuilder>
    {

        public override string Build()
        {
            if (!HasWhere())
                return "";

            return String.Format(" WHERE {0}", condition);
        }

        private bool HasWhere()
        {
            return condition != null;
        }

    }
}
