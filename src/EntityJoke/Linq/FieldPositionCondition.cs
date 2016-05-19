using System.Collections.Generic;

namespace EntityJoke.Linq
{
    public class FieldPositionCondition
    {
        public readonly List<int> Positions = new List<int>();
        private readonly string field;

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
