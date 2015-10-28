using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityJoke.Linq
{
    public abstract class SQLWithConditionGeneratorBuilder<T> where T : SQLWithConditionGeneratorBuilder<T> 
    {
        protected string condition;

        public T SetCondition(string condition)
        {
            this.condition = condition;
            return (T)this;
        }

        public abstract string Build();
    }
}
