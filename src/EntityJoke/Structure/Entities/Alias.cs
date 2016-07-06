namespace EntityJoke.Structure.Entities
{
    public class Alias
    {
        public Entity Entity;
        public string Symbol;
        public string EntityName { get { return Entity.Type.Name; } }

        public override string ToString()
        {
            return $"{EntityName}: {Symbol}";
        }
    }
}
