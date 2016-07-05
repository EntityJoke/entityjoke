using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Linq
{
    public class FieldPositionConditionExtractor
    {
        private int lastPositionFound;
        private string condition;
        private string field;
        FieldPositionCondition position;

        public FieldPositionConditionExtractor(string condition)
        {
            this.condition = (condition.ToUpper());
        }

        public FieldPositionCondition Extract(string field)
        {
            this.field = (field.ToUpper());
            Process(field);
            return position;
        }

        private void Process(string field)
        {
            position = new FieldPositionCondition(field);
            lastPositionFound = 0;

            while (ConditionHasField())
                GenerateNewPosition();

            position.Positions.Reverse();
        }

        private bool ConditionHasField()
        {
            return condition.Contains(field);
        }


        private void GenerateNewPosition()
        {
            var index = GetIndexOfField();
            AddPosition(index);
            RefreshLastPositionFound(index);
            RefreshCondition(index);
        }

        private int GetIndexOfField()
        {
            return condition.IndexOf(field);
        }

        private void AddPosition(int index)
        {
            position.Positions.Add(index + lastPositionFound);
        }

        private void RefreshLastPositionFound(int index)
        {
            lastPositionFound += index + field.Length;
        }

        private void RefreshCondition(int index)
        {
            condition = condition.Substring(index + field.Length);
        }
    }
}
