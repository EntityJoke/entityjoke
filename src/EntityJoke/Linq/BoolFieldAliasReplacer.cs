using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Linq
{
    public class BoolFieldAliasReplacer : FieldAliasReplacer
    {
        private string firstPartSQLCommand;
        private bool isActive;

        protected override string FirstPartSQLCommand(int startIndex)
        {
            firstPartSQLCommand = base.FirstPartSQLCommand(startIndex);

            VerifyValueBool();

            return isActive ? firstPartSQLCommand : RemoveNegation();
        }

        private void VerifyValueBool()
        {
            isActive = IsActive();
        }

        private bool IsActive()
        {
            return !firstPartSQLCommand.EndsWith("!");
        }

        private string RemoveNegation()
        {
            return firstPartSQLCommand.Substring(0, firstPartSQLCommand.Length - 1);
        }

        protected override string NewValue()
        {
            if (IsFieldOrderBy())
                return base.NewValue();

            return isActive ? ValueTrue() : ValueFalse();
        }

        private bool IsFieldOrderBy()
        {
            return firstPartSQLCommand.Contains(" ORDER BY ");
        }


        private string ValueTrue()
        {
            return String.Concat(base.NewValue(), " = 1");
        }

        private string ValueFalse()
        {
            return String.Concat(base.NewValue(), " = 0");
        }

    }
}
