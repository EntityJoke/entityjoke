using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Linq
{
    public class FieldPositionCondition
    {
        public List<int> Positions = new List<int>();
        private string field;

        public FieldPositionCondition(string field)
        {
            this.field = field;
        }

        public int LengthField()
        {
            return field.Length;
        }
    }
}
