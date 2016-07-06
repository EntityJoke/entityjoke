namespace EntityJoke.Structure.Fields
{
    public class FieldCollectionEntity : FieldEntity
    {
        public FieldCollectionEntity(FieldInfoCreator creator)
            : base(creator)
        {
            IsEntity = true;
            ColumnName = creator.ColumnName;
            Type = creator.Type.GenericTypeArguments[0];
        }
    }
}
