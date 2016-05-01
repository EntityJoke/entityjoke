using EntityJoke.Core;
using System;

namespace EntityJoke.Structure.Fields
{
    public class FieldCollectionEntity : FieldEntity
    {
        public Type TypeEntity;

        public FieldCollectionEntity(FieldInfoCreator creator) 
            : base(creator)
        {
            IsEntity = true;
        }

        protected override void SetColumnName(FieldInfoCreator creator)
        {
            this.ColumnName = creator.ColumnName;
        }

        protected override bool ShouldAddEntity()
        {
            return false;
        }

        protected override Type DictionaryType()
        {
            return this.Type.GenericTypeArguments[0];
        }
    }
}
