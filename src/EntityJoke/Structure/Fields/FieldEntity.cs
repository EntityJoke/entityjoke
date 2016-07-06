namespace EntityJoke.Structure.Fields
{
    public class FieldEntity : Field
    {
        public FieldEntity(FieldInfoCreator creator) : base(creator)
        {
            IsEntity = true;
            ColumnName = $"id_{creator.ColumnName}";
        }

    }
}
