using System;

namespace EntityJoke.Linq
{
    public class BoolFieldReplacer
    {
        private string fieldValue;

        public BoolFieldReplacer(string fieldValue)
        {
            this.fieldValue = fieldValue;
        }

        internal string Replace()
        {
            return IsTrue() ? ValueTrue() : ValueFalse();
        }

        private bool IsTrue()
        {
            return !fieldValue.Contains("!");
        }

        private string ValueTrue()
        {
            return String.Concat(fieldValue, " = 1");
        }

        private string ValueFalse()
        {
            return String.Concat(fieldValue, " = 0");
        }
    }
}