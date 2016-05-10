using EntityJoke.Core;
using System;

namespace EntityJoke.Structure.Fields
{
    public class FieldCollectionEntity : FieldEntity
    {
        public FieldCollectionEntity(FieldInfoCreator creator) 
            : base(creator)
        {
            IsEntity = true;
        }

        protected override void SetType(FieldInfoCreator creator)
        {
            this.Type = creator.Type.GenericTypeArguments[0];
        }

        protected override void SetColumnName(FieldInfoCreator creator)
        {
            this.ColumnName = creator.ColumnName;
        }
    }
}
