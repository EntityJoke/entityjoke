using EntityJoke.Structure;
using System;

namespace EntityJoke.Linq
{
    public class FieldAliasReplacer
    {
        private static string ALIAS_FIELD_FORMAT = "{0}.{1}";

        public string SQLQuery;
        public string EntityPath;
        public EntityJoin Entity;
        public Field Field;
        private FieldPositionCondition ocurrences;

        public string Replace()
        {
            ocurrences = OcurrencesOldValue();

            Process();

            return SQLQuery;
        }

        private void Process()
        {
            foreach (int startIndex in ocurrences.Positions)
                SQLQuery = NewSQLCommand(startIndex);
        }

        private string NewSQLCommand(int startIndex)
        {
            return String.Concat(
                FirstPartSQLCommand(startIndex), 
                NewValue(), 
                LastPartSQLCommand(startIndex));
        }

        protected virtual string FirstPartSQLCommand(int startIndex)
        {
            return SQLQuery.Substring(0, startIndex);
        }

        private string LastPartSQLCommand(int startIndex)
        {
            return SQLQuery.Substring(startIndex + ocurrences.LengthField());
        }

        private FieldPositionCondition OcurrencesOldValue()
        {
            return new FieldPositionConditionExtractor(SQLQuery)
                .Extract(OldValue());
        }

        private string OldValue()
        {
            return String.Format(ALIAS_FIELD_FORMAT, EntityWithPath(), Field.Name);
        }

        private string EntityWithPath()
        {
            return String.Concat(EntityPathFormatted(), NameEntity());
        }

        private string EntityPathFormatted()
        {
            return EntityPath != "" ? String.Concat(EntityPath,".") : "";
        }

        private string NameEntity()
        {
            return IsEntityMain() ? Entity.Field.Name : Entity.Name;
        }

        private bool IsEntityMain()
        {
            return Entity.Field != null;
        }

        protected virtual string NewValue()
        {
            return String.Format(ALIAS_FIELD_FORMAT, Entity.Alias.Symbol, Field.ColumnName);
        }

    }
}
