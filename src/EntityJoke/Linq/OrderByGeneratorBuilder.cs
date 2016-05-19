namespace EntityJoke.Linq
{
    public class OrderByGeneratorBuilder : SQLWithConditionGeneratorBuilder<OrderByGeneratorBuilder>
    {

        public override string Build()
        {
            if (condition == null)
                return "";

            return $" ORDER BY {condition}";
        }
    }
}
