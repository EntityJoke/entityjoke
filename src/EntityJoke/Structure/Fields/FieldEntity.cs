using EntityJoke.Core;
using System;

namespace EntityJoke.Structure.Fields
{
    public class FieldEntity : Field
    {
        string DeclaringTypeName;

        public FieldEntity(FieldInfoCreator creator) : base(creator)
        {
            SetDeclaringTypeName(creator);
            IsEntity = true;
        }

        protected virtual void SetDeclaringTypeName(FieldInfoCreator creator)
        {
            this.DeclaringTypeName = creator.DeclaringTypeName;
        }

        protected override void SetColumnName(FieldInfoCreator creator)
        {
            this.ColumnName = String.Format("id_{0}", creator.ColumnName);
        }

    }
}
