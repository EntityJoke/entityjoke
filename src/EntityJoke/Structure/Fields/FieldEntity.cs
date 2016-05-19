using EntityJoke.Core;
using System;

namespace EntityJoke.Structure.Fields
{
    public class FieldEntity : Field
    {
        string DeclaringTypeName;

        public FieldEntity(FieldInfoCreator creator) : base(creator)
        {
            DeclaringTypeName = creator.DeclaringTypeName;
            IsEntity = true;
            ColumnName = String.Format("id_{0}", creator.ColumnName);
        }

    }
}
