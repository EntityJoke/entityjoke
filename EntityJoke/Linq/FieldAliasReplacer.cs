using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityJoke.Linq
{
    public class FieldAliasReplacer
    {
        private static string ALIAS_FIELD_FORMAT = "{0}.{1}";

        public string SQLQuery;
        public string EntityPath;
        public EntityJoin Join;
        public Field Field;
        private FieldPositionCondition ocurrences;

        public string Replace()
        {
            ocurrences = GetOcurrencesOldValue();

            Process();

            return SQLQuery;
        }

        private void Process()
        {
            foreach (int startIndex in ocurrences.Positions)
                SQLQuery = GetNewSQLCommand(startIndex);
        }

        private string GetNewSQLCommand(int startIndex)
        {
            return String.Concat(
                GetFirstPartSQLCommand(startIndex), 
                GetNewValue(), 
                GetLastPartSQLCommand(startIndex));
        }

        private string GetFirstPartSQLCommand(int startIndex)
        {
            return SQLQuery.Substring(0, startIndex);
        }

        private string GetLastPartSQLCommand(int startIndex)
        {
            return SQLQuery.Substring(startIndex + ocurrences.LengthField());
        }

        private FieldPositionCondition GetOcurrencesOldValue()
        {
            return new FieldPositionConditionExtractor(SQLQuery)
                .Extract(GetOldValue());
        }

        private string GetOldValue()
        {
            return String.Format(ALIAS_FIELD_FORMAT, GetEntity(), Field.Name);
        }

        private string GetEntity()
        {
            return String.Concat(GetEntityPath(), GetNameEntity());
        }

        private string GetEntityPath()
        {
            return EntityPath != "" ? String.Concat(EntityPath,".") : "";
        }

        private string GetNameEntity()
        {
            return IsEntityMain() ? Join.Field.Name : Join.Name;
        }

        private bool IsEntityMain()
        {
            return Join.Field != null;
        }

        private string GetNewValue()
        {
            return String.Format(ALIAS_FIELD_FORMAT, Join.Alias.Symbol, Field.ColumnName);
        }

    }
}
