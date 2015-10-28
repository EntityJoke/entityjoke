using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Linq
{
    public interface IQuerySimpleGenerator
    {
        void SetEntity(Entity entity);
        void SetWhere(string where);
        void SetOrderBy(string orderBy);

        string GetSqlCommand();
    }
}
