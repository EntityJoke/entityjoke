namespace EntityJoke.Linq
{
    public abstract class SQLWithConditionGeneratorBuilder<T> where T : SQLWithConditionGeneratorBuilder<T>
    {
        protected string condition;

        public T SetCondition(string condition)
        {
            this.condition = condition;
            return (T)this;
        }

        public abstract string Build();
    }
}
