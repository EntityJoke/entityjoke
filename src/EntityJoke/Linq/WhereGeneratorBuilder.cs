namespace EntityJoke.Linq
{
    public class WhereGeneratorBuilder : SQLWithConditionGeneratorBuilder<WhereGeneratorBuilder>
    {

        public override string Build()
        {
            if (!HasWhere())
                return "";

            return $" WHERE {condition}";
        }

        private bool HasWhere()
        {
            return condition != null;
        }

    }
}
